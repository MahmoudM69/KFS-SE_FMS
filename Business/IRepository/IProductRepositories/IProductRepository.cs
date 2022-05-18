using Models.DTOModels.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IProductRepositories
{
    public interface IProductRepository
    {
        public Task<ProductDTO> CreateProduct(ProductDTO productDTO);
        public Task<ProductDTO> GetProduct(int productId);
        public Task<ICollection<ProductDTO>> GetAllProducts();
        public Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO);
        public void DeleteProduct(int productId);
    }
}
