using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages
{
    public class adminareaModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!(HttpContext.Session.GetString("jsonCategory")=="admin"))
                    {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
