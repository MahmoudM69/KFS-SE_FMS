using Common.Attributes;
using DataAccess.Models.SharedModels;

namespace DataAccess.Services.IRepositories.ISharedRepositories;

[Service(nameof(IProductType_FinancialAidRepository))]
public interface IProductType_FinancialAidRepository : IBaseModelRepository<ProductType_FinancialAid>
{
}
