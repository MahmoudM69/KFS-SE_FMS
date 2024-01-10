using Common.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.PaymentModels;

public class PaymentService : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Provider { get; set; } = string.Empty;
    [Required]
    public double Fee { get; set; }
    [Required]
    public double FeePercentage { get; set; }

    public virtual IEnumerable<Payment>? Payments { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
