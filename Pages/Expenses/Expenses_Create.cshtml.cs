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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Http;


namespace projekt.Pages.Expenses
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Expenses_CreateModel : PageModel
    {
        public int count;
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Expenses_CreateModel(IConfiguration configuration, ILogger<IndexModel> logger)
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
            foreach(CategoryMoney p in categories)
            {
                list.Add(p);
            }
            ViewData["ThisCategoryMoney"] = new SelectList(list, "id", "Name");
                return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) { OnGet(); return Page(); };
            newExpense.ThisCategoryMoney = ThisCategory;
            using Context _context = new Context();
            var categories = _context.CategoriesMoney
                .Where(p => p.id == idcategory)
                .FirstOrDefault();

            newExpense.ThisCategoryMoney = categories;
            _context.Add(newExpense);
            _context.SaveChanges();

            var cookieValue = Request.Cookies["MyCookie"];
            count = Int32.Parse(cookieValue);
            count++;
            Response.Cookies.Append("MyCookie", Convert.ToString(count));
            return RedirectToPage("Expenses_Index");
        }
    }
}
