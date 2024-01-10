using Microsoft.AspNetCore.Identity;
using Common.Attributes;
using DataAccess.Models.UserModels;
using DataAccess.Services.IRepositories.IUserRepositories;

namespace DataAccess.Services.Repository.UserRepositories;

[Service(nameof(IApplicationUserRepository))]
public class ApplicationUserRepository : BaseUserRepository<ApplicationUser>, IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager) : base(userManager)
    {
        _userManager = userManager;
    }
}
