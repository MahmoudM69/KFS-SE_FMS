using Common.Classes;
using Common.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.SharedModels;

public class BaseType : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
