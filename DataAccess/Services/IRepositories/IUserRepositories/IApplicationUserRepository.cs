using Common.Attributes;
using DataAccess.Models.UserModels;

namespace DataAccess.Services.IRepositories.IUserRepositories;

[Service(nameof(IApplicationUserRepository))]
public interface IApplicationUserRepository : IBaseUserRepository<ApplicationUser>
{
}
