using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            if(product != null)
            {
                if(product.ProductImages != null)
                {
                    for(int i = 0; i < product.ProductImages.Count();i++)
                    {
                            product.ProductImages[i].Product = null;
                    }
                }
                if(product.ProductType != null)
                {
                    product.ProductType = null;
                }
                var addedProduct = await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                return (addedProduct.Entity);
            }
            return null;
        }
        public async Task<Product> GetProduct(int productId)
        {
            if (productId > 0)
            {
                Product product = await _db.Products.Include(x => x.ProductImages).Include(x => x.ProductType).Include(x => x.Establishment_Products)
                                                    .Include(x => x.Orders).FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product != null)
                {
                    return (product);
                }
            }
            return null;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = await _db.Products.Include(x => x.ProductImages).Include(x => x.ProductType)
                                                              .Include(x => x.Establishment_Products).Include(x => x.Orders).ToListAsync();
            if (products.Any())
            {
                return (products);
            }
            return null;
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            if (product.ProductId > 0 && product != null)
            {
                var updatedProduct = _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return (updatedProduct.Entity);
            }
            return null;    
        }
        public void DeleteProduct(int productId)
        {
            if (productId.ToString() != null)
            {
                Product product = _db.Products.Find(productId);
                if(product != null)
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();
                }
            }
        }
    }
}
