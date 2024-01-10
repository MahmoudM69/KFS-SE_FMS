using Microsoft.AspNetCore.Http;
using Common.Interfaces;
using Common.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.SharedDTOs;

public class BaseImageDTO : IBaseDTO, ISoftDeletableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [DataType(DataType.ImageUrl)]
    public IFormFile? Image { get; set; }
    [Required]
    public string? ImageUrl { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
