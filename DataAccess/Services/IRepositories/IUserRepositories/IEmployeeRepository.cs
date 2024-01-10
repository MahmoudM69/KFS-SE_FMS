using Common.Attributes;
using DataAccess.Models.UserModels.EmployeeModels;

namespace DataAccess.Services.IRepositories.IUserRepositories;

[Service(nameof(IEmployeeRepository))]
public interface IEmployeeRepository : IBaseUserRepository<Employee>
{
}
