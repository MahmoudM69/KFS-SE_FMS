using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;

namespace Server.Pages.FinancialAidPages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DeleteModel(DataAcesss.Data.AppDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FinancialAid = await _context.FinancialAids.FindAsync(id);

            if (FinancialAid != null)
            {
                _context.FinancialAids.Remove(FinancialAid);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
