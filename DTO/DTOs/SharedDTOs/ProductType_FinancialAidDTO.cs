using DTO.DTOs.EstablishmentDTOs;
using DTO.DTOs.FinancialAidDTOs;
using DTO.DTOs.ProductDTOs;
using Common.Interfaces;
using Common.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DTOs.SharedDTOs;

public class ProductType_FinancialAidDTO : IBaseDTO, ISoftDeletableDTO, IOwnableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductTypeId { get; set; }
    [Required, ForeignKey(nameof(ProductTypeId))]
    public virtual ProductTypeDTO? ProductTypeDTO { get; set; }
    [Required]
    public int FinancialAidId { get; set; }
    [Required, ForeignKey(nameof(FinancialAidId))]
    public virtual FinancialAidDTO? FinancialAidDTO { get; set; }

    [Required]
    public int EstablishmentId { get; set; }
    public EstablishmentDTO? EstablishmentDTO { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
