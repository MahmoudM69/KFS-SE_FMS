using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;

namespace Server.Pages.EstablishmentTypePages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DeleteModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EstablishmentType EstablishmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentType = await _context.EstablishmentTypes.FirstOrDefaultAsync(m => m.EstablishmentTypeId == id);

            if (EstablishmentType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentType = await _context.EstablishmentTypes.FindAsync(id);

            if (EstablishmentType != null)
            {
                _context.EstablishmentTypes.Remove(EstablishmentType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
