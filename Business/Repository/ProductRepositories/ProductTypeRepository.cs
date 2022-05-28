using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using DataAcesss.Data.FinancialAidModels;

namespace Business.Repository.ProductRepositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly AppDbContext context;

        public ProductTypeRepository(AppDbContext db)
        {
            this.context = db;
        }
        public async Task<ProductType> CreateProductType(ProductType productType)
        {
            if(productType.ProductTypeId == 0 && productType != null)
            {               
                var dbProductType = await context.ProductTypes.AddAsync(productType);
                await context.SaveChangesAsync();
                return (dbProductType.Entity);
            }
            return null;
        }
        public async Task<ProductType> GetProductType(int productTypeId)
        {
            if(productTypeId > 0)
            {
                ProductType productType = await context.ProductTypes.Include(x => x.Products)
                                                                    .Include(x => x.ProductType_FinancialAids)
                                                                    .FirstOrDefaultAsync(x => x.ProductTypeId == productTypeId);
                if(productType != null)
                {
                    return productType;    
                }
            }
            return null;
        }
        public async Task<List<ProductType>> GetAllProductsType()
        {
            List<ProductType> productTypes = await context.ProductTypes.Include(x => x.Products)
                                                                              .Include(x => x.ProductType_FinancialAids).ToListAsync();
            if (productTypes.Any())
            {
                return productTypes;
            }
            return null;
        }
        public async Task<ProductType> UpdateProductType(int productTypeId, ProductType productType)
        {
            if(productTypeId > 0)
            {
                if(productType != null)
                {
                    var dbProductType = context.ProductTypes.Update(productType);
                    if(dbProductType.Entity != null)
                    {
                        await context.SaveChangesAsync();
                        return (dbProductType.Entity);
                    }
                    
                }
            }
            return null;
        }
        public void DeleteProductType(int productTypeId)
        {
            if(productTypeId.ToString() != null) 
            {
                ProductType productType = context.ProductTypes.Find(productTypeId);
                if(productType != null)
                {
                    context.ProductTypes.Remove(productType);
                    context.SaveChanges();
                }
            }
        }
    }
}
