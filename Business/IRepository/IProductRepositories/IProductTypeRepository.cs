using DataAcesss.Data.ProductModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.IRepository.IProductRepositories
{
    public interface IProductTypeRepository
    {
        public Task<ProductType> CreateProductType(ProductType productType);
        public Task<ProductType> GetProductType(int productTypeId);
        public Task<List<ProductType>> GetAllProductsType();
        public Task<ProductType> UpdateProductType(int productId, ProductType productType);
        public void DeleteProductType(int productTypeId);
    }
}
