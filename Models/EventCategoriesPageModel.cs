using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages
{
    [Authorize(Roles = "Admin")]
    public class EventCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;

        public void PopulateAssignedCategoryData(ProiectContext context, Event eventItem)
        {
            var allCategories = context.Category.ToList();
            var eventCategories = new HashSet<int>(eventItem.EventCategories.Select(c => c.CategoryID));

            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName, // Modificarea aici
                    Assigned = eventCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateEventCategories(ProiectContext context, string[] selectedCategories, Event eventToUpdate)
        {
            if (selectedCategories == null)
            {
                eventToUpdate.EventCategories = new List<EventCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var eventCategories = new HashSet<int>(eventToUpdate.EventCategories.Select(c => c.CategoryID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!eventCategories.Contains(cat.ID))
                    {
                        eventToUpdate.EventCategories.Add(new EventCategory
                        {
                            EventID = eventToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (eventCategories.Contains(cat.ID))
                    {
                        EventCategory categoryToRemove = eventToUpdate.EventCategories
                            .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(categoryToRemove);
                    }
                }
            }
        }
    }
}