using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper mapper;
        public ProductRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            this.mapper = mapper;
        }
        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            if(productDTO != null)
            {
                Product product = mapper.Map<ProductDTO, Product>(productDTO);
                var addedProduct = await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                return mapper.Map<Product, ProductDTO>(addedProduct.Entity);
            }
            return null;
        }
        public async Task<ProductDTO> GetProduct(int productId)
        {
            if (productId.ToString() != null)
            {
                Product product = await _db.Products.Include(x => x.ProductImages).Include(x => x.ProductType).Include(x => x.Establishment_Products)
                                                    .Include(x => x.Orders).FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product != null)
                {
                    ProductDTO productDTO = mapper.Map<ProductDTO>(product);
                    return productDTO;
                }
            }
            return null;
        }
        public async Task<ICollection<ProductDTO>> GetAllProducts()
        {
            ICollection<Product> products = await _db.Products.Include(x => x.ProductImages).Include(x => x.ProductType)
                                                              .Include(x => x.Establishment_Products).Include(x => x.Orders).ToListAsync();
            if (products.Any())
            {
                ICollection<ProductDTO> productDTOs = mapper.Map< ICollection<ProductDTO>>(products);
                return productDTOs;
            }
            return null;
        }
        public async Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO)
        {
            if (productId.ToString() != null && productDTO != null)
            {
                Product product = await _db.Products.Include(x => x.ProductImages).Include(x => x.ProductType)
                                                    .Include(x => x.Orders).Include(x => x.Establishment_Products)
                                                    .FirstOrDefaultAsync(x => x.ProductId == productId);
                productDTO.Establishment_ProductDTOs ??= mapper.Map<ICollection<Establishment_ProductDTO>>(product.Establishment_Products);
                productDTO.OrderDTOs ??= mapper.Map<ICollection<OrderDTO>>(product.Orders);
                productDTO.ProductImageDTOs ??= mapper.Map<ICollection<ProductImageDTO>>(product.ProductImages);
                productDTO.ProductTypeDTO ??= mapper.Map<ProductTypeDTO>(product.ProductType);
                product = mapper.Map<ProductDTO, Product>(productDTO, product);
                var updatedProduct = _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return mapper.Map<Product, ProductDTO>(updatedProduct.Entity);
            }
            return null;    
        }
        public async void DeleteProduct(int productId)
        {
            if (productId.ToString() != null)
            {
                Product product = await _db.Products.FindAsync(productId);
                if(product != null)
                {
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
