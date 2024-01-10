using Common.Attributes;
using DataAccess.Models.ProductModels;

namespace DataAccess.Services.IRepositories.IProductRepositories;

[Service(nameof(IProductRepository))]
public interface IProductRepository : IBaseModelRepository<Product>
{
}
