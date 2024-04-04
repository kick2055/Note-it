using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace projekt.Pages.Admin
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Role_Account_ObserverModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }
        public string role = "observer";

        public Role_Account_ObserverModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            if (ModelState.IsValid == false) return Page();
            string myCompanyDBcs = _configuration.GetConnectionString("AccountsDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "UPDATE logins SET category =@role WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@role", role);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("Role_Account");
        }
    }
}
