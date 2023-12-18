using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Events
{
    public class CreateModel : EventCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
           // ViewData["ContactID"] = new SelectList(_context.Contact, "ID", "ID");
            var newEvent = new Event();
            AssignedCategoryDataList = new List<AssignedCategoryData>();

            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newEvent = new Event();
            if (selectedCategories != null)
            {
                newEvent.EventCategories = new List<EventCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new EventCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newEvent.EventCategories.Add(catToAdd);
                }
            }
            Event.EventCategories = newEvent.EventCategories;
            _context.Event.Add(Event);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}