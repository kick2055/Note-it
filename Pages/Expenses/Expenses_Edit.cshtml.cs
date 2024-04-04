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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_EditModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty]
        public Expense newExpense { get; set; }
        [BindProperty]
        public int idcategory { get; set; }
        public List<CategoryMoney> list = new List<CategoryMoney>();
        public CategoryMoney ThisCategory { get; set; }
        public IActionResult OnGet()
        {
            if (!(HttpContext.Session.GetString("jsonCategory") == "admin" || HttpContext.Session.GetString("jsonCategory") == "editor"))
            {
                return RedirectToPage("Expenses_Index");
            }
            using Context context = new Context();
            var categories = context.CategoriesMoney
                ;
            foreach (CategoryMoney p in categories)
            {
                list.Add(p);
            }
            ViewData["ThisCategoryMoney"] = new SelectList(list, "id", "Name");
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) return Page();
            using Context context = new Context();
            var editedone = context.Expenses
                .Include(x => x.ThisCategoryMoney)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            var category = context.CategoriesMoney
                .Where(d => d.id == idcategory)
                .FirstOrDefault();
            if (editedone is Expense)
            {
                editedone.ThisCategoryMoney = category;
                editedone.Date = newExpense.Date;
                editedone.Name = newExpense.Name;
                editedone.Price = newExpense.Price;
            }

            context.SaveChanges();
            return RedirectToPage("Expenses_Index");
        }
    }
}
