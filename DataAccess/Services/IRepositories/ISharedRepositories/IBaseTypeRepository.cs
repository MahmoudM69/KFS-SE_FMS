using Common.Attributes;
using Common.Interfaces.Entities;

namespace DataAccess.Services.IRepositories.ISharedRepositories;

[Service(nameof(IBaseTypeRepository<T>))]
public interface IBaseTypeRepository<T> : IBaseModelRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
}
