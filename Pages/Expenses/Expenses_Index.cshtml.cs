using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Models;
using projekt.Data;
using Microsoft.EntityFrameworkCore;
using projekt.ModelsNoMigration;

namespace projekt.Pages.Expenses
{
    public class Expenses_IndexModel : PageModel
    {
        public int count;
        public int countDelete;

        public List<Expense> wydatki = new List<Expense>();
        public void OnGet([FromServices] ICounter myCounter)
        {
            countDelete = myCounter.GetCounter();
            var cookieValue = Request.Cookies["MyCookie"];
            if (cookieValue == null)
            {
                Response.Cookies.Append("MyCookie", "0");
                count = 0;
            }
            else
            {
                count = Int32.Parse(cookieValue);
            }
            
            
            using Context context = new Context();
            var Expenses = context.Expenses
                .Include(x => x.ThisCategoryMoney)
                .OrderBy(p => p.Date);
            foreach(Expense p in Expenses)
            {

                wydatki.Add(p);
            }
        }
    }
}
