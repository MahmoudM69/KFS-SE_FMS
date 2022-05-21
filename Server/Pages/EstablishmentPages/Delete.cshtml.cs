﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DeleteModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Establishment Establishment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Establishment = await _context.Establishments
                .Include(e => e.EstablishmentType).FirstOrDefaultAsync(m => m.EstablishmentId == id);

            if (Establishment == null)
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

            Establishment = await _context.Establishments.FindAsync(id);

            if (Establishment != null)
            {
                _context.Establishments.Remove(Establishment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
