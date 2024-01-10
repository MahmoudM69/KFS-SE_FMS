using Common.Interfaces.Entities;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.SharedModels;

public class ProductType_FinancialAid : IBaseEntity, ISoftDeletableEntity, IOwnableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductTypeId { get; set; }
    [Required, ForeignKey(nameof(ProductTypeId))]
    public virtual ProductType? ProductType { get; set; }
    [Required]
    public int FinancialAidId { get; set; }
    [Required, ForeignKey(nameof(FinancialAidId))]
    public virtual FinancialAid? FinancialAid { get; set; }
    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
