using DTO.DTOs.SharedDTOs;
using Common.Interfaces;
using Common.Interfaces.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.ProductDTOs;

public class ProductDTO : IBaseDTO, ISoftDeletableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the product name")]
    [Display(Name = "Product Name")]
    public string? Name { get; set; }
    [Display(Name = "Product Description")]
    public string? Description { get; set; }

    [Required]
    public IEnumerable<ProductTypeDTO>? ProductTypeDTOs { get; set; }
    public IEnumerable<ProductImageDTO>? ProductImageDTOs { get; set; }
    public IEnumerable<Establishment_ProductDTO>? Establishment_ProductDTOs { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
