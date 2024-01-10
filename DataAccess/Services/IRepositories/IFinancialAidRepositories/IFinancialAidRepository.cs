using Common.Attributes;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Services.IRepositories;

namespace DataAccess.Services.IRepositories.IFinancialAidRepositories;

[Service(nameof(IFinancialAidRepository))]
public interface IFinancialAidRepository : IBaseModelRepository<FinancialAid>
{
}
