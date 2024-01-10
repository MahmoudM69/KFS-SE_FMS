using Common.Interfaces.Entities;

namespace DTO.DTOs.BaseDTOs;

public class OwnableEntityDTO : SoftDeletableEntityDTO, IOwnableEntity
{
    public int EstablishmentId { get; set; }
}
