using DTO.DTOs.ProductDTOs;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.IServices.IProductServices;

[Service(nameof(IProductDTOService))]
public interface IProductDTOService : IBaseDTOService<ProductDTO, Product, IProductRepository>
{

}
