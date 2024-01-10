using Common.Interfaces;
using Common.Interfaces.Entities;

namespace DTO.DTOs.BaseDTOs;

public class BaseDTO : IBaseEntity, IValidatable
{
    public int Id { get; set; }
}
