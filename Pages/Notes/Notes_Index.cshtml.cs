using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using projekt.Data;
using projekt.Models;

namespace projekt.Pages.Notes
{
    public class Notes_IndexModel : PageModel
    {
        public List<Note> notes = new List<Note>();
        public void OnGet()
        {
            using Context context = new Context();
            var Notes = context.Notes
                .OrderBy(p => p.Name);
            foreach (Note p in Notes)
            {
                notes.Add(p);
            }
        }
    }
}
