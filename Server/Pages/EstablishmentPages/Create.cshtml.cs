﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;

namespace Server.Pages.EstablishmentPages
{
    public class CreateModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public CreateModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EstablishmentTypeId"] = new SelectList(_context.EstablishmentTypes, "EstablishmentTypeId", "Type");
            return Page();
        }

        [BindProperty]
        public Establishment Establishment { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Establishments.Add(Establishment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}