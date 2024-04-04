using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projekt.Data;
using projekt.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace projekt.Pages.Notes
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Notes_CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public Notes_CreateModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {

            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty]
        public Note newNote { get; set; }
        public IActionResult OnGet()
        {
            if (!(HttpContext.Session.GetString("jsonCategory") == "admin" || HttpContext.Session.GetString("jsonCategory") == "editor"))
            {
                return RedirectToPage("Notes_Index");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) return Page();
            using Context _context = new Context();
            _context.Add(newNote);
            _context.SaveChanges();
            return RedirectToPage("Notes_Index");
        }
    }
}
