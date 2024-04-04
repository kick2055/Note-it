using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using projekt.ModelsNoMigration;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;


namespace projekt
{
    public class UserLoginModel : PageModel
    {

        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string jsonCategory { get; set; }
        public void OnGet()
        {
        }
        protected string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        private bool ValidateUser(SiteUser user)
        {
            int id, w = 0;
            string login, password, category;
            List<account> konta = new List<account>();
            string myCompanyDBcs = _configuration.GetConnectionString("AccountsDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM logins";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder htmlStr = new StringBuilder("");
            while (reader.Read())
            {
                id = Int32.Parse(reader["Id"].ToString());
                login = reader["login"].ToString();
                password = reader["password"].ToString();
                category = reader["category"].ToString();
                konta.Add(new account { Id = id, Login = String.Concat(login.Where(c => !Char.IsWhiteSpace(c))), Password = String.Concat(password.Where(c => !Char.IsWhiteSpace(c))),Category = category });
            }
            reader.Close(); con.Close();
            foreach (account konto in konta)
            {
                if ((user.userName == konto.Login) && (HashPassword(user.password) == konto.Password))
                {
                    HttpContext.Session.SetString("jsonCategory", konto.Category);
                    w = 1;
                }
            }
            if (w == 1)
                return true;
            else
                return false;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
 {
 new Claim(ClaimTypes.Name, user.userName)
 };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
               ClaimsPrincipal(claimsIdentity));
                return Page();
            }
            return Page();
        }
    }
}
