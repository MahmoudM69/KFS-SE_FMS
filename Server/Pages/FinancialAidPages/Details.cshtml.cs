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
    public class DetailsModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DetailsModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
