﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.PaymentModels;

namespace Server.Pages.PaymentServicesPages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public DeleteModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentService PaymentService { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentService = await _context.PaymentServices.FirstOrDefaultAsync(m => m.PaymentServiceId == id);

            if (PaymentService == null)
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

            PaymentService = await _context.PaymentServices.FindAsync(id);

            if (PaymentService != null)
            {
                _context.PaymentServices.Remove(PaymentService);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
