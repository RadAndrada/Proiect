using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Proiect.Models.ViewModels;

namespace Proiect.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public CategoryIndexData CategoryData { get; set; }

        public async Task OnGetAsync()
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(c => c.EventCategories)
                .ThenInclude(ec => ec.Event)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task OnGetByIdAsync(int? id)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(i => i.EventCategories)
                .ThenInclude(ec => ec.Event)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                    .FirstOrDefault(i => i.ID == id.Value);

                if (category != null)
                {
                    CategoryData.Events = category.Events;
                }
            }
        }

        public int CategoryID { get; set; }
        public int EventID { get; set; }
    }
}
