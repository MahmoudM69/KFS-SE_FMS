using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.PaymentDTOs;

public class PaymentServiceDTO : IBaseDTO, ISoftDeletableDTO, IValidatable
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Payment Service Name")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Payment Service Provider")]
    public string Provider { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Payment Service Fee")]
    public double Fee { get; set; }
    [Required]
    public double FeePercentage { get; set; }

    public IEnumerable<PaymentDTO>? PaymentDTOs { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
