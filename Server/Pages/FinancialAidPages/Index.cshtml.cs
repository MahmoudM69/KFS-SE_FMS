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
    public class IndexModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public IndexModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<FinancialAid> FinancialAid { get;set; }

        public async Task OnGetAsync()
        {
            FinancialAid = await _context.FinancialAids
                .Include(f => f.Establishment).ToListAsync();
        }
    }
}
