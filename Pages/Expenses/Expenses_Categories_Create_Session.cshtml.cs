using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using projekt.Models;
using projekt.Data;

namespace projekt.Pages.Expenses
{
    public class Expenses_Categories_Create_SessionModel : PageModel
    {
        public CategoryMoney deletedcategory;


        public CategoryMoney deletedcategorynew = new CategoryMoney { };
        public IActionResult OnGet()
        {
            if (ModelState.IsValid == false) return Page();
            if(!(HttpContext.Session.GetString("jsonDB") is null))
            {
                deletedcategory = JsonSerializer.Deserialize<CategoryMoney>(HttpContext.Session.GetString("jsonDB"));
                using Context context = new Context();
                deletedcategorynew.Name = deletedcategory.Name;
                context.Add(deletedcategorynew);
                context.SaveChanges();
            }  
            return RedirectToPage("Expenses_Categories");
        }
    }
}
