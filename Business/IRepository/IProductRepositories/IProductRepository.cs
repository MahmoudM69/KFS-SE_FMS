using DataAcesss.Data.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IProductRepositories
{
    public interface IProductRepository
    {
        public Task<Product> CreateProduct(Product product);
        public Task<Product> GetProduct(int productId);
        public Task<List<Product>> GetAllProducts();
        public Task<Product> UpdateProduct(Product product);
        public void DeleteProduct(int productId);
    }
}
