using Common.Interfaces.Entities;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.OrderModels;
using DataAccess.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.SharedModels;

public class Establishment_Product : IBaseEntity, ISoftDeletableEntity, IOwnableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public float Quantity { get; set; }
    [Required]
    public double PurchasePrice { get; set; }
    [Required]
    public double RetailPrice { get; set; }
    [Required]
    public double AidPercentage { get; set; }
    [Required]
    public double AidAmount { get; set; }
    [Required]
    public DateTime ProductionDate { get; set; } = DateTime.Now;
    public DateTime? ExpirationDate { get; set; }

    [Required]
    public int ProductId { get; set; }
    [Required, ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }

    public virtual IEnumerable<Order>? Orders { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
