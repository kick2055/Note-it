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
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages.Notes
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Notes_DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Notes_DeleteModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            if (!(HttpContext.Session.GetString("jsonCategory") == "admin" || HttpContext.Session.GetString("jsonCategory") == "editor"))
            {
                return RedirectToPage("Notes_Index");
            }
            using Context context = new Context();
            var deletedone = context.Notes
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (deletedone is Note)
            {
                context.Remove(deletedone);
            }

            context.SaveChanges();

            return RedirectToPage("Notes_Index");
        }
    }
}
