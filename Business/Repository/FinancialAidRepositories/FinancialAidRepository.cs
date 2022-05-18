using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Business.IRepository.IFinancialAidRepositories;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Models.DTOModels.FinancialAidDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.OrderDTOs;

namespace Business.Repository.FinancialAidRepositories
{
    public class FinancialAidRepository : IFinancialAidRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public FinancialAidRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FinancialAidDTO> CreateFinancialAid(FinancialAidDTO financialAidDTO)
        {
            if(financialAidDTO != null && financialAidDTO.EstablishmentDTO != null)
            {
                FinancialAid financialAid = mapper.Map<FinancialAid>(financialAidDTO);
                foreach(ProductType productType in mapper.Map<ICollection<ProductType>>(financialAidDTO.ProductTypeDTOs))
                {
                    ProductType_FinancialAid productType_FinancialAid = new()
                    {
                        ProductTypeId = productType.ProductTypeId,
                        ProductType = productType,
                        FinancialAidId = financialAid.FinancialAidId,
                        FinancialAid = financialAid
                    };
                    financialAid.ProductType_FinancialAids.Add(productType_FinancialAid);
                }
                var dbFinancialAid = await context.FinancialAids.AddAsync(financialAid);
                await context.SaveChangesAsync();
                return mapper.Map<FinancialAidDTO>(dbFinancialAid);
            }
            return null;
        }

        public async Task<FinancialAidDTO> GetFinancialAid(int id)
        {
            if(id.ToString() != null)
            {
                FinancialAid financialAid = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .FirstOrDefaultAsync(x => x.FinancialAidId == id);
                if(financialAid != null)
                {
                    FinancialAidDTO financialAidDTO = mapper.Map<FinancialAidDTO>(financialAid);
                    foreach(ProductType_FinancialAid productType_FinancialAid in financialAid.ProductType_FinancialAids)
                    {
                        financialAidDTO.ProductTypeDTOs.Add(mapper.Map<ProductTypeDTO>(productType_FinancialAid.ProductType));
                    }

                }
            }
            return null;
        }

        public async Task<ICollection<FinancialAidDTO>> GetAllFinancialAids()
        {
            ICollection<FinancialAid> financialAids = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders).ToListAsync();
            if (financialAids.Any())
            {
                List<FinancialAidDTO> financialAidDTOs = new();
                foreach(FinancialAid financialAid in financialAids)
                {
                    FinancialAidDTO financialAidDTO = mapper.Map<FinancialAidDTO>(financialAid);
                    foreach (ProductType_FinancialAid productType_FinancialAid in financialAid.ProductType_FinancialAids)
                    {
                        financialAidDTO.ProductTypeDTOs.Add(mapper.Map<ProductTypeDTO>(productType_FinancialAid.ProductType));
                    }
                    financialAidDTOs.Add(financialAidDTO);
                }
                return financialAidDTOs;
            }
            return null;
        }

        public async Task<ICollection<FinancialAidDTO>> GetEstablishmentFinancialAids(int id)
        {
            if(id.ToString() != null)
            {
                ICollection<FinancialAid> financialAids = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .Where(x => x.EstablishmentId == id)
                                                                       .ToListAsync();
                if (financialAids.Any())
                {
                    List<FinancialAidDTO> financialAidDTOs = new();
                    foreach (FinancialAid financialAid in financialAids)
                    {
                        FinancialAidDTO financialAidDTO = mapper.Map<FinancialAidDTO>(financialAid);
                        foreach (ProductType_FinancialAid productType_FinancialAid in financialAid.ProductType_FinancialAids)
                        {
                            financialAidDTO.ProductTypeDTOs.Add(mapper.Map<ProductTypeDTO>(productType_FinancialAid.ProductType));
                        }
                        financialAidDTOs.Add(financialAidDTO);
                    }
                    return financialAidDTOs;
                }
            }
            return null;
        }

        public async Task<ICollection<FinancialAidDTO>> GetProductTypeFinancialAids(int id)
        {
            if (id.ToString() != null)
            {
                ICollection<FinancialAid> financialAids = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .Where(x => x.ProductType_FinancialAids.FirstOrDefault(y => y.ProductTypeId == id).ProductTypeId == id)
                                                                       .ToListAsync();
                if (financialAids.Any())
                {
                    List<FinancialAidDTO> financialAidDTOs = new();
                    foreach (FinancialAid financialAid in financialAids)
                    {
                        FinancialAidDTO financialAidDTO = mapper.Map<FinancialAidDTO>(financialAid);
                        foreach (ProductType_FinancialAid productType_FinancialAid in financialAid.ProductType_FinancialAids)
                        {
                            financialAidDTO.ProductTypeDTOs.Add(mapper.Map<ProductTypeDTO>(productType_FinancialAid.ProductType));
                        }
                        financialAidDTOs.Add(financialAidDTO);
                    }
                    return financialAidDTOs;
                }
            }
            return null;
        }

        public async Task<FinancialAidDTO> UpdateFinancialAid(int id, FinancialAidDTO financialAidDTO)
        {
            if (id.ToString() != null && financialAidDTO != null && financialAidDTO.EstablishmentDTO != null)
            {
                FinancialAid financialAid = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .FirstOrDefaultAsync(x => x.FinancialAidId == id);
                if(financialAid != null)
                {
                    financialAidDTO.EstablishmentDTO ??= mapper.Map<EstablishmentDTO>(financialAid.Establishment);
                    financialAidDTO.OrderDTOs ??= mapper.Map<ICollection<OrderDTO>>(financialAid.ProductType_FinancialAids);
                    //financialAidDTO.ProductTypeDTOs ??= mapper.Map<ICollection<ProductTypeDTO>>(financialAid.ProductType_FinancialAids.Where(x => x.FinancialAidId == financialAid.FinancialAidId));
                    financialAid = mapper.Map<FinancialAidDTO, FinancialAid>(financialAidDTO, financialAid);
                    if (financialAidDTO.ProductTypeDTOs.Any())
                    {
                        financialAid.ProductType_FinancialAids.Clear();
                        foreach (var ProductTypeDTO in financialAidDTO.ProductTypeDTOs)
                        {
                            ProductType_FinancialAid ProductType_FinancialAid = new()
                            {
                                ProductTypeId = ProductTypeDTO.ProductTypeId,
                                ProductType = mapper.Map<ProductType>(ProductTypeDTO),
                                FinancialAidId = financialAid.FinancialAidId,
                                FinancialAid = financialAid

                            };
                            financialAid.ProductType_FinancialAids.Add(ProductType_FinancialAid);
                        }
                    }
                    var dbFinancialAid = context.FinancialAids.Update(financialAid);
                    await context.SaveChangesAsync();
                    return mapper.Map<FinancialAidDTO>(dbFinancialAid);
                }
            }
            return null;
        }

        public async void DeleteFinancialAid(int id)
        {
            if(id.ToString() != null)
            {
                FinancialAid financialAid = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).FirstOrDefaultAsync(x => x.FinancialAidId == id);
                if(financialAid != null)
                {
                    context.FinancialAids.Remove(financialAid);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
