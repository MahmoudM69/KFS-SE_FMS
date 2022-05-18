using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOModels.ProductDTOs;

namespace Business.IRepository.IProductRepositories
{
    public interface IProductTypeRepository
    {
        public Task<ProductTypeDTO> CreateProductType(ProductTypeDTO productTypeDTO);
        public Task<ProductTypeDTO> GetProductType(int productTypeId);
        public Task<ICollection<ProductTypeDTO>> GetAllProductsType();
        public Task<ProductTypeDTO> UpdateProductType(int productId, ProductTypeDTO productTypeDTO);
        public void DeleteProductType(int productTypeId);
    }
}
