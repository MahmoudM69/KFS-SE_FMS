using Microsoft.AspNetCore.Identity;
using Common.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Services.IRepositories.IUserRepositories;
using DataAccess.Models.UserModels.EmployeeModels;

namespace DataAccess.Services.Repository.UserRepositories;

[Service(nameof(IEmployeeRepository))]
public class EmployeeRepository : BaseUserRepository<Employee>, IEmployeeRepository
{
    private readonly UserManager<Employee> _userManager;

    public EmployeeRepository(UserManager<Employee> userManager) : base(userManager)
    {
        _userManager = userManager;
    }


    public override async Task<Employee> CreateAsync(Employee user, string password, CancellationToken cancellationToken = default)
    {
        return await base.CreateAsync(user, password, new List<Claim>()
        {
            new("EstablishmentId", user.EstablishmentId.ToString())
        },
        false, cancellationToken);
    }

    public override async Task<Employee> CreateAsync(Employee user, string password, IEnumerable<Claim> claims, bool addDefaultClaims = true, CancellationToken cancellationToken = default)
    {
        claims = claims.Append(new Claim("EstablishmentId", user.EstablishmentId.ToString()));
        return await base.CreateAsync(user, password, claims, addDefaultClaims, cancellationToken);
    }
}
