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
using Microsoft.AspNetCore.Identity;
using DataAcesss.Data.EmployeeModels;

namespace Server.Pages.FinancialAidPages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public IList<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }

        public async Task OnGetAsync()
        {
            if (HttpContext.User.IsInRole("Manager"))
            {
                Employee user = (Employee)await userManager.GetUserAsync(HttpContext.User);
                ProductType_FinancialAids = await _context.ProductType_FinancialAids
                                                          .Include(x => x.FinancialAid).ThenInclude(y => y.Establishment)
                                                          .Include(x => x.ProductType).Where(x => x.FinancialAid.EstablishmentId == user.EstablishmentId).ToListAsync();
            }
            else if (HttpContext.User.IsInRole("Admin"))
            {
                ProductType_FinancialAids = await _context.ProductType_FinancialAids
                                                          .Include(x => x.FinancialAid).ThenInclude(y => y.Establishment)
                                                          .Include(x => x.ProductType).ToListAsync();
            }
        }
    }
}
