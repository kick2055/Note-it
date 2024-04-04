using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.ModelsNoMigration;
using System.Security.Cryptography;
using System.Text;
using projekt.Pages;
using System.Data;

namespace projekt.Pages.Admin
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Create_AccountModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Create_AccountModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {

            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty]
        public accountcheck newaccountcheck { get; set; }
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
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false || newaccountcheck.Password != newaccountcheck.PasswordAgain)  return Page();
            


            

            string account_string =
_configuration.GetConnectionString("AccountsDB");
            SqlConnection con = new SqlConnection(account_string);
            SqlCommand cmd = new SqlCommand("add_account", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter login_param = new SqlParameter("@login", SqlDbType.NVarChar,
           int.MaxValue);
            login_param.Value = newaccountcheck.Login;
            cmd.Parameters.Add(login_param);
            SqlParameter password_param = new SqlParameter("@password", SqlDbType.NVarChar,int.MaxValue);
            password_param.Value = HashPassword(newaccountcheck.Password);
            cmd.Parameters.Add(password_param);
            SqlParameter category_param = new SqlParameter("@category", SqlDbType.NVarChar, int.MaxValue);
            category_param.Value = "observer";
            cmd.Parameters.Add(category_param);
            SqlParameter accountID_param = new SqlParameter("@accountID",
           SqlDbType.Int);
            accountID_param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(accountID_param);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("adminarea");
        }
    }
}
