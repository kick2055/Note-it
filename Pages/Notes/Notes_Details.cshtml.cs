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

namespace projekt.Pages.Notes
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class Notes_DetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IConfiguration _configuration { get; }

        public Notes_DetailsModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {

            _logger = logger;
            _configuration = configuration;
        }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public Note ReadNote = new Note { };
        public void OnGet()
        {
            using Context context = new Context();
            var _ReadNote = context.Notes
                .Where(p => p.Id == id)
                .FirstOrDefault();
            if (_ReadNote is Note)
            {
                ReadNote = _ReadNote;
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false) return Page();
            return RedirectToPage("Notes_Index");
        }
    }
}
