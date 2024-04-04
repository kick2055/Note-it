using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.ModelsNoMigration;

namespace projekt.Pages.Admin
{
    public class Role_AccountModel : PageModel
    {
        public List<account> Accounts = new List<account>();
        public string Login;
        public string Role;
        public int Id;
        public string Password;
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Role_AccountModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("AccountsDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM logins";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder htmlStr = new StringBuilder("");
            while (reader.Read())
            {
                Id = Int32.Parse(reader["Id"].ToString());
                Login = reader.GetString(1);
                Password = reader.GetString(2);
                Role = reader.GetString(3);
                Accounts.Add(new account { Id = Id, Login = Login, Password = Password, Category = Role });
            }
            reader.Close(); con.Close();
        }
    }
}
