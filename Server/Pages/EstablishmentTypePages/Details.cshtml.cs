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
    public class DetailsModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DetailsModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public EstablishmentType EstablishmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentType = await _context.EstablishmentTypes.FirstOrDefaultAsync(m => m.EstablishmentTypeId == id);

            if (EstablishmentType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
