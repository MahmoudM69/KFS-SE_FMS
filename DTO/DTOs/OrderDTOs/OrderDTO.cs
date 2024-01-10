using System.ComponentModel.DataAnnotations;
using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.FinancialAidDTOs;
using DTO.DTOs.PaymentDTOs;
using DTO.DTOs.SharedDTOs;
using DTO.DTOs.UserDTOs.CustomerDTOs;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.OrderDTOs;

public class OrderDTO : IBaseDTO, ISoftDeletableDTO, IOwnableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the quantity")]
    public float Quantity { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Required]
    public string? CustomerId { get; set; }
    [Required]
    public CustomerDTO? CustomerDTO { get; set; }
    [Required]
    public int Establishment_ProductId { get; set; }
    [Required]
    public Establishment_ProductDTO? Establishment_ProductDTO { get; set; }
    public int PaymentId { get; set; }
    public PaymentDTO? PaymentDTO { get; set; }
    public int FinancialAidId { get; set; }
    public FinancialAidDTO? FinancialAidDTO { get; set; }

    [Required]
    public int EstablishmentId { get; set; }
    public EstablishmentDTO? EstablishmentDTO { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
