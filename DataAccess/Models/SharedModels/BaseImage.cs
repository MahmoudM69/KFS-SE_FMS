using Common.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.SharedModels;

public class BaseImage : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
