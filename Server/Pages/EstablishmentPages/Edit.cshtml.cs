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

namespace Server.Pages.EstablishmentPages
{
    public class EditModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public EditModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Establishment Establishment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Establishment = await _context.Establishments
                .Include(e => e.EstablishmentType).FirstOrDefaultAsync(m => m.EstablishmentId == id);

            if (Establishment == null)
            {
                return NotFound();
            }
           ViewData["EstablishmentTypeId"] = new SelectList(_context.EstablishmentTypes, "EstablishmentTypeId", "Type");
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

            _context.Attach(Establishment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentExists(Establishment.EstablishmentId))
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

        private bool EstablishmentExists(int id)
        {
            return _context.Establishments.Any(e => e.EstablishmentId == id);
        }
    }
}
