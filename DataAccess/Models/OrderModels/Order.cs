using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Models.PaymentModels;
using DataAccess.Models.SharedModels;
using DataAccess.Models.UserModels.CustomerModels;
using Common.Enums;
using Common.Interfaces.Entities;

namespace DataAccess.Models.OrderModels;

public class Order : IBaseEntity, ISoftDeletableEntity, IOwnableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public float Quantity { get; set; }
    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Required]
    public string CustomerId { get; set; } = string.Empty;
    [Required, ForeignKey(nameof(CustomerId))]
    public virtual Customer? Customer { get; set; }

    [Required]
    public int Establishment_ProductId { get; set; }
    [Required, ForeignKey(nameof(Establishment_ProductId))]
    public virtual Establishment_Product? Establishment_Product { get; set; }

    public int? PaymentId { get; set; }
    [ForeignKey(nameof(PaymentId))]
    public virtual Payment? Payment { get; set; }

    public int? FinancialAidId { get; set; }
    [ForeignKey(nameof(FinancialAidId))]
    public virtual FinancialAid? FinancialAid { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;

    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }
}
