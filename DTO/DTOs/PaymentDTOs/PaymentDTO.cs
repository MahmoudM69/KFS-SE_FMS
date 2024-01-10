using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.OrderDTOs;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.PaymentDTOs;

public class PaymentDTO : IBaseDTO, ISoftDeletableDTO, IOwnableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime? Date { get; set; } = DateTime.Now;
    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    [Required]
    public int PaymentServiceId { get; set; }
    public PaymentServiceDTO? PaymentServiceDTO { get; set; }
    public IEnumerable<OrderDTO>? OrderDTOs { get; set; }

    [Required]
    public int EstablishmentId { get; set; }
    public EstablishmentDTO? EstablishmentDTO { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
