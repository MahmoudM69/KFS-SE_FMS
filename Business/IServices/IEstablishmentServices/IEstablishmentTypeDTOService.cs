using DTO.DTOs.EstablishmentDTOs;
using Common.Attributes;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;

namespace Business.IServices.IEstablishmentServices;

[Service(nameof(IEstablishmentTypeDTOService))]
public interface IEstablishmentTypeDTOService : IBaseDTOService<EstablishmentTypeDTO, EstablishmentType, IEstablishmentTypeRepository>
{
}
