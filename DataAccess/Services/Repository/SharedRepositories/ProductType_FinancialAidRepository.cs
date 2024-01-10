using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Models.SharedModels;
using DataAccess.Services.IRepositories.ISharedRepositories;

namespace DataAccess.Services.Repository.SharedRepositories;

[Service(nameof(IProductType_FinancialAidRepository))]
public class ProductType_FinancialAidRepository : BaseModelRepository<ProductType_FinancialAid>, IProductType_FinancialAidRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductType_FinancialAidRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
