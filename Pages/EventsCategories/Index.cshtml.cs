using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.EventsCategories
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<EventCategory> EventCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            EventCategory = await _context.EventCategory
                .Include(e => e.Category)
                .Include(e => e.Event).ToListAsync();
        }
    }
}
