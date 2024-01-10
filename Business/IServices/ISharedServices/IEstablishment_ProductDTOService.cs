using DTO.DTOs.SharedDTOs;
using Common.Attributes;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Models.SharedModels;

namespace Business.IServices.ISharedServices;

[Service(nameof(IEstablishment_ProductDTOService))]
public interface IEstablishment_ProductDTOService : IBaseDTOService<Establishment_ProductDTO, Establishment_Product, IEstablishment_ProductRepository>
{
}
