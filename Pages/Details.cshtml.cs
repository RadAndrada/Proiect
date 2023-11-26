using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectContext _context;

        public DetailsModel(ProiectContext context)
        {
            _context = context;
        }

        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventItem = await _context.Event.FirstOrDefaultAsync(m => m.ID == id);

            if (eventItem == null)
            {
                return NotFound();
            }
            else
            {
                Event = eventItem;
            }

            return Page();
        }
    }
}
