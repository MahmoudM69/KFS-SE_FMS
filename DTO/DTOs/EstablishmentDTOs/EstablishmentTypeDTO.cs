using DTO.DTOs.SharedDTOs;
using System.Collections.Generic;

namespace DTO.DTOs.EstablishmentDTOs;

public class EstablishmentTypeDTO : BaseTypeDTO
{
    public IEnumerable<EstablishmentDTO>? EstablishmentDTOs { get; set; }
}
