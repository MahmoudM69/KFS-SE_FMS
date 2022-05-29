using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.PaymentModels;

namespace Server.Pages.PaymentPages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public IndexModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; }

        public async Task OnGetAsync()
        {
            Payment = await _context.Payments
                .Include(p => p.PaymentService).ToListAsync();
        }
    }
}
