using DataAcesss.Data.ProductModels;
using Models.DTOModels.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IProductRepositories
{
    public interface IProductImageRepository
    {
        public Task<ProductImage> CreateProductImage(ProductImageDTO image);
        public void DeleteProductImageByImageId(int imageId);
        public void DeleteProductImageByProductId(int productId);
        public void DeleteProductImageByImageUrl(string imageUrl);
        public Task<IEnumerable<ProductImageDTO>> GetProductImages(int productId);

    }
}
