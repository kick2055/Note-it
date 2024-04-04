using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.ModelsNoMigration;

namespace projekt.Pages.Expenses
{
    
    public class Expenses_ShowcaseModel : PageModel
    {
        public FromTo result;
        public void OnGet(FromTo cap)
        {
            result = cap;
        }
    }
}
