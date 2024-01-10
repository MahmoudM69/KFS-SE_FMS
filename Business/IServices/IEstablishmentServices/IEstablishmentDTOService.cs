using DTO.DTOs.EstablishmentDTOs;
using Common.Attributes;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Models.EstablishmentModels;

namespace Business.IServices.IEstablishmentServices;

[Service(nameof(IEstablishmentDTOService))]
public interface IEstablishmentDTOService : IBaseDTOService<EstablishmentDTO, Establishment, IEstablishmentRepository>
{
}
