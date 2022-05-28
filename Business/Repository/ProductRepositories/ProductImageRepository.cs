using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.ProductModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.ProductRepositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext context;
        public ProductImageRepository(AppDbContext db)
        {
            context = db;
        }

        public async Task<ProductImage> CreateProductImage(ProductImage image)
        {
            if(image != null)
            {
                var dbImage = await context.ProductImages.AddAsync(image);
                await context.SaveChangesAsync();
                return dbImage.Entity;
            }
            return null;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(int productId)
        {
            if(productId > 0)
            {
                List<ProductImage> productImages = await context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
                if (productImages != null && productImages.Any())
                {
                    return productImages;
                }
            }
            return null;
        }

        public async void DeleteProductImageByImageId(int imageId)
        {
            if(imageId.ToString() != null)
            {
                var image = await context.ProductImages.FindAsync(imageId);
                if( image != null)
                {
                    context.ProductImages.Remove(image);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async void DeleteProductImageByImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var images = await context.ProductImages.FirstOrDefaultAsync
                    (x => x.ProductImageUrl.ToLower() == imageUrl.ToLower());
                if (images != null)
                {
                    context.ProductImages.Remove(images);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async void DeleteProductImageByProductId(int productId)
        {
            if(productId.ToString() != null)
            {
                var imageList = await context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
                if(imageList.Any())
                {
                    context.ProductImages.RemoveRange(imageList);
                    context.SaveChanges();
                }
            }
        }
    }
}
