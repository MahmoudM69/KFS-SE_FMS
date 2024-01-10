using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Interfaces.Entities;

namespace DataAccess.Models.EstablishmentModels;

public class EstablishmentType : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public virtual IEnumerable<Establishment>? Establishments { get; set; }

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
