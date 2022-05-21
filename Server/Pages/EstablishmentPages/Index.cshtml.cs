using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;

namespace Server.Pages.EstablishmentPages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public IndexModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Establishment> Establishment { get;set; }

        public async Task OnGetAsync()
        {
            Establishment = await _context.Establishments
                .Include(e => e.EstablishmentType).ToListAsync();
        }
    }
}
