using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.Data;
using projekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_Categories_DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public string jsonDB { get; set; }

        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_Categories_DeleteModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            using Context context = new Context();
            var deletedone = context.CategoriesMoney
                .Where(p => p.id == Id)
                .FirstOrDefault();
            if (deletedone is CategoryMoney)
            {
                var expense = context.Expenses
                    .Include(x => x.ThisCategoryMoney)
                    .Where(p => p.ThisCategoryMoney == deletedone)
                    .FirstOrDefault();
                    
                if(!(expense is Expense))
                {
                    HttpContext.Session.SetString("jsonDB", JsonSerializer.Serialize(deletedone));
                    context.Remove(deletedone);
                }
                else
                {
                    return Page();
                }
                
            }

            context.SaveChanges();

            return RedirectToPage("Expenses_Categories");
        }
    }      
}
