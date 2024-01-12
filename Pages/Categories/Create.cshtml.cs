using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;
using Proiect.Models;
using System.Threading.Tasks;

namespace Proiect.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ProiectContext _context;

        public CreateModel(ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Categoria a fost adăugată cu succes!";

            return RedirectToPage("./Index");
        }
    }
}
