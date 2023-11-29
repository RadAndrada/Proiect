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

        public IndexModel(ProiectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IList<Event> Events { get; set; } = new List<Event>();
        public EventData EventD { get; set; }
        public int EventID { get; set; }
        public int CategoryID { get; set; }
        public async Task OnGetAsync( int? id, int? categoryID)
        {

            EventD = new EventData();

            EventD.Events = await _context.Event
                .Include(b => b.Contact)
                .Include(b => b.EventCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            if (id != null)
            {
                EventID = id.Value;
                Event myEvent = EventD.Events.FirstOrDefault(i => i.ID == id.Value);

                if (myEvent != null)
                {
                    EventD.Categories = myEvent.EventCategories.Select(ec => ec.Category);
                }
            }

        }
    }
}
