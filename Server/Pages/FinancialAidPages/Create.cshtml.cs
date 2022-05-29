using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.Shared;

namespace Server.Pages.FinancialAidPages
{
    public class CreateModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;

        public CreateModel(DataAcesss.Data.AppDbContext context)
        {
            _context = context;
        }

        public class InputModel
        {
            [Required, Range(0, double.MaxValue)]
            public decimal MinBalance { get; set; }
            [Required, Range(0, double.MaxValue)]
            public decimal MaxBalance { get; set; }
            public bool Percentage { get; set; }
            [Required, Range(0, double.MaxValue)]
            public decimal AidAmount { get; set; }
            [Required]
            public int EstablishmentId { get; set; }
            [Required]
            public int ProductTypeId { get; set; }

        }

        public IActionResult OnGet()
        {
            ViewData["EstablishmentId"] = new SelectList(_context.Establishments, "EstablishmentId", "EstablishmentName");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Type");
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            FinancialAid financialAid = new()
            {
                FinancialAidId = 0,
                MinBalance = Input.MinBalance,
                MaxBalance = Input.MaxBalance,
                Percentage = Input.Percentage,
                AidAmount = Input.AidAmount,
                EstablishmentId = Input.EstablishmentId
            };
            var addedFinancialAid = await _context.FinancialAids.AddAsync(financialAid);
            await _context.SaveChangesAsync();
            ProductType_FinancialAid productType_FinancialAid = new()
            {
                FinancialAidId = addedFinancialAid.Entity.FinancialAidId,
                ProductTypeId = Input.ProductTypeId
            };
            await _context.ProductType_FinancialAids.AddAsync(productType_FinancialAid);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
