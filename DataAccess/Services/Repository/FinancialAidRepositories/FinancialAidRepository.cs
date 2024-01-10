using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.Repository;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Services.IRepositories.IFinancialAidRepositories;

namespace DataAccess.Services.Repository.FinancialAidRepositories;

[Service(nameof(IFinancialAidRepository))]
public class FinancialAidRepository : BaseModelRepository<FinancialAid>, IFinancialAidRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public FinancialAidRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
