using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.OrderModels;
using DataAccess.Models.SharedModels;
using Common.Interfaces.Entities;

namespace DataAccess.Models.FinancialAidModels;

public class FinancialAid : IBaseEntity, ISoftDeletableEntity, IOwnableEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public double MinBalance { get; set; }
    [Required]
    public double MaxBalance { get; set; }
    [Required]
    public double AidAmount { get; set; }
    [Required]
    public double AidPercentage { get; set; }
    [Required]
    public double Budget { get; set; }

    public virtual IEnumerable<Order>? Orders { get; set; }
    public virtual IEnumerable<ProductType_FinancialAid>? ProductType_FinancialAids { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;

    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }
}
