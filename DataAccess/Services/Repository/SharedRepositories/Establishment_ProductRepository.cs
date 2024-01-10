using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Models.SharedModels;

namespace DataAccess.Services.Repository.SharedRepositories;

[Service(nameof(IEstablishment_ProductRepository))]
public class Establishment_ProductRepository : BaseModelRepository<Establishment_Product>, IEstablishment_ProductRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public Establishment_ProductRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
