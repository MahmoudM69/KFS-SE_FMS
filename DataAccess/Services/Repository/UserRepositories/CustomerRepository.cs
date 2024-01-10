using Microsoft.AspNetCore.Identity;
using Common.Attributes;
using DataAccess.Models.UserModels.CustomerModels;
using DataAccess.Services.Repository;
using DataAccess.Services.IRepositories.IUserRepositories;

namespace DataAccess.Services.Repository.UserRepositories;

[Service(nameof(ICustomerRepository))]
public class CustomerRepository : BaseUserRepository<Customer>, ICustomerRepository
{
    private readonly UserManager<Customer> _userManager;

    public CustomerRepository(UserManager<Customer> userManager) : base(userManager)
    {
        _userManager = userManager;
    }
}
