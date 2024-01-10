using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;
using DataAccess;
using DataAccess.Services.Repository.SharedRepositories;

namespace DataAccess.Services.Repository.ProductRepositories;

[Service(nameof(IProductTypeRepository))]
public class ProductTypeRepository : BaseTypeRepository<ProductType>, IProductTypeRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductTypeRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
