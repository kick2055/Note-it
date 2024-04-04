using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Expenses
{
    public class Expenses_DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public Expense expense { get; set; }

        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_DetailsModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            using Context context = new Context();
            var chosenone = context.Expenses
                  .Include(x => x.ThisCategoryMoney)
                  .Where(p => p.Id == id)
                  .FirstOrDefault();
            expense = chosenone;
        }
    }
}
