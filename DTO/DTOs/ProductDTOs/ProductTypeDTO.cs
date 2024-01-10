using DTO.DTOs.SharedDTOs;
using System.Collections.Generic;

namespace DTO.DTOs.ProductDTOs;

public class ProductTypeDTO : BaseTypeDTO
{
    public ICollection<ProductDTO>? ProductDTOs { get; set; }
    public ICollection<ProductType_FinancialAidDTO>? ProductType_FinancialAidDTOs { get; set; }
}
