using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectContext _context;

        public DetailsModel(ProiectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myEvent = await _context.Event.FirstOrDefaultAsync(m => m.ID == id);

            if (myEvent == null)
            {
                return NotFound();
            }

            Event = myEvent;

            return Page();
        }
    }
}
