using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Events
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ProiectContext _context;

        public DeleteModel(ProiectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
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
            else
            {
                Event = myEvent;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}