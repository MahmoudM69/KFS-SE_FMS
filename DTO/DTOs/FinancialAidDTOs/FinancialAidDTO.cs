using DTO.DTOs.BaseDTOs;
using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.OrderDTOs;
using DTO.DTOs.SharedDTOs;
using Common.Interfaces;
using Common.Interfaces.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.FinancialAidDTOs;

public class FinancialAidDTO : IBaseDTO, ISoftDeletableDTO, IOwnableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Minimum Balance")]
    public double MinBalance { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Maximum Balance")]
    public double MaxBalance { get; set; }
    [Required]
    [Display(Name = "Aid Amount")]
    public double AidAmount { get; set; }
    [Required]
    [Display(Name = "Aid Percentage")]
    public double AidPercentage { get; set; }
    [Required]
    public double Budget { get; set; }

    [Required]
    public int EstablishmentId { get; set; }
    public EstablishmentDTO? EstablishmentDTO { get; set; }
    public IEnumerable<OrderDTO>? OrderDTOs { get; set; }
    public IEnumerable<ProductType_FinancialAidDTO>? ProductType_FinancialAidDTOs { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
