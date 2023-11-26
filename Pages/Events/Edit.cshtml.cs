using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace Proiect.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ProiectContext _context;

        public EditModel(ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            Event = existingEvent;
            ViewData["ContactID"] = new SelectList(_context.Set<Contact>(), "ID",
"ContactEmail");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingEvent = await _context.Event.FirstOrDefaultAsync(m => m.ID == Event.ID);

            if (existingEvent == null)
            {
                return NotFound();
            }

            // Actualizați doar proprietățile care s-au schimbat
            existingEvent.ID = Event.ID;
            existingEvent.Name = Event.Name;
            existingEvent.Description = Event.Description;
            existingEvent.Location = Event.Location;
            existingEvent.Date = Event.Date;
            existingEvent.Price = Event.Price;
            existingEvent.Contact = Event.Contact;
            // Repetați pentru celelalte proprietăți

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.ID == id);
        }
    }
}
