using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Server.Pages.RolePages
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [Required, BindProperty]
        public string RoleName { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IdentityRole identityRole = new IdentityRole()
            {
                Name = RoleName
            };
            IdentityResult identityResult = await roleManager.CreateAsync(identityRole);

            return RedirectToPage("./Index");
        }
    }
}
