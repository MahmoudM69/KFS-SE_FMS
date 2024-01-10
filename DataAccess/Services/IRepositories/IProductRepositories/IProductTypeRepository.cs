using Common.Attributes;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Models.ProductModels;

namespace DataAccess.Services.IRepositories.IProductRepositories;

[Service(nameof(IProductTypeRepository))]
public interface IProductTypeRepository : IBaseTypeRepository<ProductType>
{
}
