using DTO.DTOs.ProductDTOs;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.IServices.IProductServices;

[Service(nameof(IProductTypeDTOService))]
public interface IProductTypeDTOService : IBaseDTOService<ProductTypeDTO, ProductType, IProductTypeRepository>
{
}
