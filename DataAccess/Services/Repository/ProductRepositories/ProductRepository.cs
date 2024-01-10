using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Services.Repository;
using DataAccess.Models.ProductModels;

namespace DataAccess.Services.Repository.ProductRepositories;

[Service(nameof(IProductRepository))]
public class ProductRepository : BaseModelRepository<Product>, IProductRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
