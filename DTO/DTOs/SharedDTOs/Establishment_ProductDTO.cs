using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.ProductDTOs;
using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.OrderDTOs;
using Common.Interfaces.DTOs;
using Common.Interfaces;

namespace DTO.DTOs.SharedDTOs;

public class Establishment_ProductDTO : IBaseDTO, ISoftDeletableDTO, IOwnableDTO, IValidatable
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
    public double AidAmount { get; set; }
    [Required]
    public double AidPercentage { get; set; }
    [Required]
    public DateTime ProductionDate { get; set; } = DateTime.Now;
    public DateTime? ExpirationDate { get; set; } = DateTime.Now;

    [Required]
    public int ProductId { get; set; }
    [Required]
    public virtual ProductDTO? ProductDTO { get; set; }
    [Required]
    public int EstablishmentId { get; set; }
    [Required]
    public virtual EstablishmentDTO? EstablishmentDTO { get; set; }
    
    public virtual ICollection<OrderDTO>? OrderDTOs { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
