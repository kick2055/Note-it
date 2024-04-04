using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.Models;
using projekt.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using projekt.ModelsNoMigration;

namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        

        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_DeleteModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet([FromServices] ICounter myCounter)
        {
            if (!(HttpContext.Session.GetString("jsonCategory") == "admin" || HttpContext.Session.GetString("jsonCategory") == "editor"))
            {
                return RedirectToPage("Expenses_Index");
            }

            using Context context = new Context();
            var deletedone = context.Expenses
                .Where(p => p.Id == id)
                .Include(x => x.ThisCategoryMoney)
                .FirstOrDefault();
            if(deletedone is Expense)
            {
                context.Remove(deletedone);
            }
            
            context.SaveChanges();
            myCounter.IncCounter();
            return RedirectToPage("Expenses_Index");
        }
    }
}
