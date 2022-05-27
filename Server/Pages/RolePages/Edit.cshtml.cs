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
using DataAcesss.Data.Shared;
using Microsoft.AspNetCore.Authorization;

namespace Server.Pages.RolePages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public EditModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [Required, BindProperty]
        public IdentityRole Role { get; set; } = new();
        [Required, BindProperty]
        public List<UserRoleEditModel> Users { get; set; } = new();
        public async Task<IActionResult> OnGet(string id)
        {
            var users = userManager.Users.ToList();
            Role = await roleManager.FindByIdAsync(id);
            foreach (var user in users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, Role.Name))
                {
                    UserRoleEditModel userRoleEditModel = new()
                    {
                        UserId = user.Id,
                        User = user,
                        IsChecked = true
                    };
                    Users.Add(userRoleEditModel);
                }
                else
                {
                    UserRoleEditModel userRoleEditModel = new()
                    {
                        UserId = user.Id,
                        User = user,
                        IsChecked = false
                    };
                    Users.Add(userRoleEditModel);
                }
            };
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await roleManager.UpdateAsync(Role);
            foreach(UserRoleEditModel user in Users)
            {
                user.User = await userManager.FindByIdAsync(user.UserId);
                Role = await roleManager.FindByIdAsync(Role.Id);
                if (user.IsChecked && !(await userManager.IsInRoleAsync(user.User, Role.Name)))
                {
                    await userManager.AddToRoleAsync(user.User, Role.Name);
                }
                else if (!user.IsChecked && await userManager.IsInRoleAsync(user.User, Role.Name))
                {
                    await userManager.RemoveFromRoleAsync(user.User, Role.Name);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToPage("./Index");
        }
    }
    
    public class UserRoleEditModel
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsChecked { get; set; }
    }
}
