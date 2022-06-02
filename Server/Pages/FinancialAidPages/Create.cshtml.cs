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
using Microsoft.AspNetCore.Identity;
using DataAcesss.Data.EmployeeModels;

namespace Server.Pages.FinancialAidPages
{
    public class CreateModel : PageModel
    {
        private readonly DataAcesss.Data.AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateModel(DataAcesss.Data.AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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

        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnGetAsync()
        {
            if (HttpContext.User.IsInRole("Admin"))
                ViewData["EstablishmentId"] = new SelectList(context.Establishments, "EstablishmentId", "EstablishmentName");

            ViewData["ProductTypeId"] = new SelectList(context.ProductTypes, "ProductTypeId", "Type");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (HttpContext.User.IsInRole("Manager"))
            {
                Employee manager = context.Employees.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
                Input.EstablishmentId = manager.EstablishmentId;
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
            var addedFinancialAid = await context.FinancialAids.AddAsync(financialAid);
            await context.SaveChangesAsync();
            ProductType_FinancialAid productType_FinancialAid = new()
            {
                FinancialAidId = addedFinancialAid.Entity.FinancialAidId,
                ProductTypeId = Input.ProductTypeId
            };
            await context.ProductType_FinancialAids.AddAsync(productType_FinancialAid);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
