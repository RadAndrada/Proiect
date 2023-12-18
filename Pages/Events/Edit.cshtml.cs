using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Events
{
    [Authorize(Roles = "Admin")]
    public class EditModel : EventCategoriesPageModel
    {
        private readonly ProiectContext _context;

        public EditModel(ProiectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public Event Event { get; set; } = new Event(); // Inițializează instanța Event

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Event
                .Include(b => b.EventCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Event == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Event);
            //ViewData["ContactID"] = new SelectList(_context.Contact, "ID", "ID");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventToUpdate = await _context.Event
                .Include(i => i.EventCategories)
                    .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (eventToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Event>(
                eventToUpdate,
                "Event",
                i => i.Name,
                i => i.Price,
                i => i.Location,
                i => i.Date,
                i => i.ContactEmail))
            {
                UpdateEventCategories(_context, selectedCategories, eventToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateEventCategories(_context, selectedCategories, eventToUpdate);
            PopulateAssignedCategoryData(_context, eventToUpdate);
            return Page();
        }
    }
}
