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
using projekt.ModelsNoMigration;

namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_CalculationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_CalculationModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty]
        public FromTo interval { get; set; }
        public decimal Number=0;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            using Context context = new Context();
            foreach(Expense p in context.Expenses)
            {
                interval.Sum += 1;
                if (DateTime.Compare(p.Date,interval.DateFrom)>0 && DateTime.Compare(p.Date, interval.DateTo) < 0)
                {
                    Number += p.Price;
                    
                }
            }
            interval.Sum = Number;
            return RedirectToPage("Expenses_Showcase",interval);
        }
    }
}
