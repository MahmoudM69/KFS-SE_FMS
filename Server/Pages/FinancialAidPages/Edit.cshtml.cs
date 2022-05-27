using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;

namespace Server.Pages.FinancialAidPages
{
    public class EditModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public EditModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FinancialAid FinancialAid { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FinancialAid = await _context.FinancialAids
                .Include(f => f.Establishment).FirstOrDefaultAsync(m => m.FinancialAidId == id);

            if (FinancialAid == null)
            {
                return NotFound();
            }
           ViewData["EstablishmentId"] = new SelectList(_context.Establishments, "EstablishmentId", "EstablishmentAddress");
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

            _context.Attach(FinancialAid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialAidExists(FinancialAid.FinancialAidId))
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

        private bool FinancialAidExists(int id)
        {
            return _context.FinancialAids.Any(e => e.FinancialAidId == id);
        }
    }
}
