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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventToUpdate = await _context.Event
                .Include(e => e.EventCategories)
                .FirstOrDefaultAsync(e => e.ID == id);

            if (eventToUpdate == null)
            {
                return NotFound();
            }

            eventToUpdate.Name = Event.Name;
            eventToUpdate.Description = Event.Description; // Adaugă această linie
            eventToUpdate.Location = Event.Location;
            eventToUpdate.Date = Event.Date;
            eventToUpdate.Price = Event.Price;
            eventToUpdate.ContactEmail = Event.ContactEmail;

            // Update categories
            UpdateEventCategories(_context, selectedCategories, eventToUpdate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Event.FirstOrDefault(e => e.ID == id.Value) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw; // Handle the exception as needed
                }
            }

            return RedirectToPage("./Index");
        }

    }
}