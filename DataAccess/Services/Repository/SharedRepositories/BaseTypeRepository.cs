using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.Repository;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Models.SharedModels;
using DataAccess;
using Common.Interfaces.Entities;

namespace DataAccess.Services.Repository.SharedRepositories;

[Service(nameof(IBaseTypeRepository<T>))]
public class BaseTypeRepository<T> : BaseModelRepository<T>, IBaseTypeRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public BaseTypeRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
