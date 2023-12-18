using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context ;
        }

        public IList<Event> Events { get; set; } = new List<Event>();
        public EventData EventD { get; set; }
        public int EventID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync( int? id, int? categoryID, string sortOrder, string searchString)
        {

            EventD = new EventData();

            Name = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            CurrentFilter = searchString;

            EventD.Events = await _context.Event
                .Include(b => b.EventCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                EventD.Events = EventD.Events.Where(s => s.Name.Contains(searchString) || s.Name.Contains(searchString));
            }
            if (id != null)
            {
                EventID = id.Value;
                Event myEvent = EventD.Events.FirstOrDefault(i => i.ID == id.Value);

                if (myEvent != null)
                {
                    EventD.Categories = myEvent.EventCategories.Select(ec => ec.Category);
                }
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    EventD.Events = EventD.Events.OrderByDescending(s => s.Name);
                    break;
                default: EventD.Events = EventD.Events.OrderBy(s => s.Name);
                    break;

            }
        }
    }
}
