using DTO.DTOs.SharedDTOs;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.ProductDTOs;

public class ProductImageDTO : BaseImageDTO
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public virtual ProductDTO? ProductDTO { get; set; }
}
