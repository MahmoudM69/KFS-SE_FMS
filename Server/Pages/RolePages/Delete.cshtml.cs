using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Server.Pages.RolePages
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public DeleteModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [BindProperty]
        public IdentityRole IdentityRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityRole = await roleManager.FindByIdAsync(id);

            if (IdentityRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityRole = await roleManager.FindByIdAsync(id);

            if (IdentityRole != null)
            {
                await roleManager.DeleteAsync(IdentityRole);
            }

            return RedirectToPage("./Index");
        }
    }
}
