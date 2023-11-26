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
            _context = context;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingEvent = await _context.Event.FirstOrDefaultAsync(m => m.ID == id);

            if (existingEvent == null)
            {
                return NotFound();
            }
            else
            {
                Event = existingEvent;
            }

            return Page();
        }
    }
}
