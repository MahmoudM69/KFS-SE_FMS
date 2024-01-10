using System.ComponentModel.DataAnnotations;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.SharedDTOs;

public class BaseTypeDTO : IBaseDTO, ISoftDeletableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Type { get; set; }
    public string? Description { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
