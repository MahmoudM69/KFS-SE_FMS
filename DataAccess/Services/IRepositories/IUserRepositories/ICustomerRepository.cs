using Common.Attributes;
using DataAccess.Models.UserModels.CustomerModels;
using DataAccess.Services.IRepositories;

namespace DataAccess.Services.IRepositories.IUserRepositories;

[Service(nameof(ICustomerRepository))]
public interface ICustomerRepository : IBaseUserRepository<Customer>
{
}
