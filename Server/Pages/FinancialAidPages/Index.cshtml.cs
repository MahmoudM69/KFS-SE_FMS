using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.Shared;

namespace Server.Pages.FinancialAidPages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public IndexModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }

        public async Task OnGetAsync()
        {
            ProductType_FinancialAids = await _context.ProductType_FinancialAids
                                                      .Include(x => x.FinancialAid).ThenInclude(y => y.Establishment)
                                                      .Include(x => x.ProductType).ToListAsync();
        }
    }
}
