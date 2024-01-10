using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.EstablishmentModels;

[Index(nameof(ImageUrl))]
public class EstablishmentImage : IBaseEntity, ISoftDeletableEntity, IOwnableEntity 
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public int EstablishmentId { get; set; }

    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
