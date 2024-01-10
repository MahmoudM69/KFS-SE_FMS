using DTO.DTOs.SharedDTOs;
using Common.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.EstablishmentDTOs;

public class EstablishmentImageDTO : BaseImageDTO, IOwnableEntity
{
    [Required]
    public int EstablishmentId { get; set; }
    public EstablishmentDTO? EstablishmentDTO { get; set; }
}
