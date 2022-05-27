using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Server.Pages.RolePages
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public List<IdentityRole> IdentityRoles { get; set; }

        public async Task OnGetAsync()
        {
            IdentityRoles = await roleManager.Roles.ToListAsync();
        }
    }
}
