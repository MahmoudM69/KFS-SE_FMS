using DTO.DTOs.SharedDTOs;
using Common.Attributes;
using DataAccess.Models.SharedModels;
using DataAccess.Services.IRepositories.ISharedRepositories;

namespace Business.IServices.ISharedServices;

[Service(nameof(IProductType_FinancialAidDTOService))]
public interface IProductType_FinancialAidDTOService : IBaseDTOService<ProductType_FinancialAidDTO, ProductType_FinancialAid, IProductType_FinancialAidRepository>
{
}
