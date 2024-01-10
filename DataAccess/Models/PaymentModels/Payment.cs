using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.OrderModels;
using Common.Enums;
using Common.Interfaces.Entities;

namespace DataAccess.Models.PaymentModels;

public class Payment : IBaseEntity, ISoftDeletableEntity, IOwnableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    [Required]
    public int PaymentServiceId { get; set; }
    [Required, ForeignKey(nameof(PaymentServiceId))]
    public virtual PaymentService? PaymentService { get; set; }

    [Required]
    public virtual IEnumerable<Order>? Orders { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;

    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }
}
