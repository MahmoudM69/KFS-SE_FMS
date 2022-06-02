using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.Shared;
using Microsoft.AspNetCore.Identity;
using DataAcesss.Data.EmployeeModels;

namespace Server.Pages.FinancialAidPages
{
    public class EditModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public EditModel(DataAcesss.Data.AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public class InputModel
        {
            [Required]
            public int FinancialAidId { get; set; }
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

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductType_FinancialAid productType_FinancialAid = await _context.ProductType_FinancialAids
                                    .Include(x => x.FinancialAid).ThenInclude(y => y.Establishment)
                                    .Include(x => x.ProductType)
                                    .FirstOrDefaultAsync(x => x.FinancialAidId == id);
            InputModel preInput = new()
            {
                MaxBalance = productType_FinancialAid.FinancialAid.MaxBalance,
                MinBalance = productType_FinancialAid.FinancialAid.MinBalance,
                AidAmount = productType_FinancialAid.FinancialAid.AidAmount,
                Percentage = productType_FinancialAid.FinancialAid.Percentage,
                FinancialAidId = id.Value,
                ProductTypeId = productType_FinancialAid.ProductTypeId,
                EstablishmentId = productType_FinancialAid.FinancialAid.EstablishmentId
            };
            Input = preInput;
            if (Input == null)
            {
                return NotFound();
            }
            ViewData["EstablishmentId"] = new SelectList(_context.Establishments, "EstablishmentId", "EstablishmentName");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "Type");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.User.IsInRole("Manager"))
            {
                Employee manager = _context.Employees.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
                Input.EstablishmentId = manager.EstablishmentId;
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!FinancialAidExists(Input.FinancialAidId))
            {
                return NotFound();
            }

            FinancialAid financialAid = new()
            {
                FinancialAidId = Input.FinancialAidId,
                MinBalance = Input.MinBalance,
                MaxBalance = Input.MaxBalance,
                Percentage = Input.Percentage,
                AidAmount = Input.AidAmount,
                EstablishmentId = Input.EstablishmentId
            };
            //_context.Attach(financialAid).State = EntityState.Modified;
            var addedFinancialAid = _context.FinancialAids.Update(financialAid);
            await _context.SaveChangesAsync();
            ProductType_FinancialAid productType_FinancialAid = new()
            {
                FinancialAidId = financialAid.FinancialAidId,
                ProductTypeId = Input.ProductTypeId
            };
            //_context.Attach(productType_FinancialAid).State = EntityState.Modified;
            _context.ProductType_FinancialAids.Update(productType_FinancialAid);
            await _context.SaveChangesAsync();

            //_context.Attach(Input).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!FinancialAidExists(Input.FinancialAidId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private bool FinancialAidExists(int id)
        {
            return _context.FinancialAids.Any(e => e.FinancialAidId == id);
        }
    }
}
