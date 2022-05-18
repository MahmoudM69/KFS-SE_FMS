using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.ProductModels;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.ProductDTOs;
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
        private readonly IMapper mapper;
        public ProductImageRepository(AppDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            context = db;
        }

        public async Task<ProductImage> CreateProductImage(ProductImageDTO imageDTO)
        {
            if(imageDTO != null)
            {
                var image = mapper.Map<ProductImageDTO, ProductImage>(imageDTO);
                if(image != null)
                {
                    var dbImage = await context.ProductImages.AddAsync(image);
                    await context.SaveChangesAsync();
                    return dbImage.Entity;
                }
            }
            return null;
        }

        public async Task<IEnumerable<ProductImageDTO>> GetProductImages(int productId)
        {
            if(productId.ToString() != null)
            {
                return mapper.Map<IEnumerable<ProductImage>, IEnumerable<ProductImageDTO>>(
                    await context.ProductImages.Where(x => x.ProductId == productId).ToListAsync());
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
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
