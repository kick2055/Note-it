using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;
using projekt.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages.Expenses
{
    public class Expenses_CategoriesModel : PageModel
    {
        public List<CategoryMoney> categories = new List<CategoryMoney>();
        public IActionResult OnGet()
        {
            if (!(HttpContext.Session.GetString("jsonCategory") == "admin" || HttpContext.Session.GetString("jsonCategory") == "editor"))
            {
                return RedirectToPage("Expenses_Index");
            }
            using Context context = new Context();
            var category = context.CategoriesMoney
                ;
            foreach (CategoryMoney p in category)
            {
                categories.Add(p);
            }
            return Page();
        }
    }
}
