using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;

namespace Server.Pages.EstablishmentTypePages
{
    public class EditModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public EditModel(DataAcesss.Data.AppDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EstablishmentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentTypeExists(EstablishmentType.EstablishmentTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EstablishmentTypeExists(int id)
        {
            return _context.EstablishmentTypes.Any(e => e.EstablishmentTypeId == id);
        }
    }
}
