using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;

namespace Server.Pages.EstablishmentTypePages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public IndexModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<EstablishmentType> EstablishmentType { get;set; }

        public async Task OnGetAsync()
        {
            EstablishmentType = await _context.EstablishmentTypes.ToListAsync();
        }
    }
}
