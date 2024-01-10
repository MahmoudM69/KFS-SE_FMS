using Common.Interfaces.Entities;

namespace DTO.DTOs.BaseDTOs;

public class SoftDeletableEntityDTO : BaseDTO, ISoftDeletableEntity
{
    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
