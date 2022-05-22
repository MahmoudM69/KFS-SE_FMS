using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Business.IRepository.IProductRepositories;
using DataAcesss.Data;
using DataAcesss.Data.ProductModels;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.FinancialAidDTOs;
using DataAcesss.Data.Shared;
using DataAcesss.Data.FinancialAidModels;

namespace Business.Repository.ProductRepositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ProductTypeRepository(AppDbContext db, IMapper mapper)
        {
            this.context = db;
            this.mapper = mapper;
        }
        public async Task<ProductTypeDTO> CreateProductType(ProductTypeDTO productTypeDTO)
        {
            if(productTypeDTO != null)
            {
                ProductType productType = mapper.Map<ProductType>(productTypeDTO);
                if(productTypeDTO.FinancialAidDTOs != null)
                {
                    foreach (FinancialAidDTO financialAidDTO in productTypeDTO.FinancialAidDTOs)
                    {
                        ProductType_FinancialAid productType_FinancialAid = new()
                        {
                            ProductTypeId = productType.ProductTypeId,
                            ProductType = productType,
                            FinancialAidId = financialAidDTO.FinancialAidId,
                            FinancialAid = mapper.Map<FinancialAid>(financialAidDTO)
                        };
                        productType.ProductType_FinancialAids.Add(productType_FinancialAid);
                    }
                }
                
                var dbProductType = await context.ProductTypes.AddAsync(productType);
                await context.SaveChangesAsync();
                return mapper.Map<ProductTypeDTO>(dbProductType.Entity);
            }
            return null;
        }
        public async Task<ProductTypeDTO> GetProductType(int productTypeId)
        {
            if(productTypeId.ToString() != null)
            {
                ProductType productType = await context.ProductTypes.Include(x => x.Products)
                                                                    .Include(x => x.ProductType_FinancialAids)
                                                                    .FirstOrDefaultAsync(x => x.ProductTypeId == productTypeId);
                if(productType != null)
                {
                    ProductTypeDTO productTypeDTO = mapper.Map<ProductTypeDTO>(productType);
                    return productTypeDTO;    
                }
            }
            return null;
        }
        public async Task<ICollection<ProductTypeDTO>> GetAllProductsType()
        {
            ICollection<ProductType> productTypes = await context.ProductTypes.Include(x => x.Products)
                                                                              .Include(x => x.ProductType_FinancialAids).ToListAsync();
            if (productTypes.Any())
            {
                ICollection<ProductTypeDTO> productTypeDTOs = mapper.Map<ICollection<ProductTypeDTO>>(productTypes);
                if (productTypeDTOs.Any())
                {
                    return productTypeDTOs;
                }
            }
            return null;
        }
        public async Task<ProductTypeDTO> UpdateProductType(int productTypeId, ProductTypeDTO productTypeDTO)
        {
            if(productTypeId.ToString() != null && productTypeDTO != null)
            {
                ProductType productType = await context.ProductTypes.Include(x => x.Products)
                                                                    .Include(x => x.ProductType_FinancialAids)
                                                                    .FirstOrDefaultAsync(x => x.ProductTypeId == productTypeId);
                if(productType != null)
                {
                    productTypeDTO.ProductDTOs ??= mapper.Map<ICollection<ProductDTO>>(productType.Products);
                    if (!productTypeDTO.FinancialAidDTOs.Any())
                    {
                        foreach(ProductType_FinancialAid productType_FinancialAid in productType.ProductType_FinancialAids)
                        {
                            productTypeDTO.FinancialAidDTOs.Add(mapper.Map<FinancialAidDTO>(productType_FinancialAid.FinancialAid));
                        }
                    }
                    ProductType updatedProductType = mapper.Map<ProductTypeDTO, ProductType>(productTypeDTO, productType);
                    var dbProductType = context.ProductTypes.Update(updatedProductType);
                    if(dbProductType.Entity != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<ProductTypeDTO>(dbProductType.Entity);
                    }
                    
                }
            }
            return null;
        }
        public async void DeleteProductType(int productTypeId)
        {
            if(productTypeId.ToString() != null) 
            {
                ProductType productType = await context.ProductTypes.FindAsync(productTypeId);
                if(productType != null)
                {
                    context.ProductTypes.Remove(productType);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
