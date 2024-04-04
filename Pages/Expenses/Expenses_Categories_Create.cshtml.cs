using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Models;
using projekt.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_Categories_CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_Categories_CreateModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty]
        public CategoryMoney newCategory { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) return Page();
            using Context context = new Context();
            context.Add(newCategory);
            context.SaveChanges();
            return RedirectToPage("Expenses_Categories");
        }
    }
}
