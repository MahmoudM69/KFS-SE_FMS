using Models.DTOModels.FinancialAidDTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOModels.ProductDTOs
{
    public class ProductTypeDTO
    {
        public int ProductTypeId { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public string Type { get; set; }
        [Display(Name = "Product Type Description")]
        public string Description { get; set; }
        public ICollection<int> ProductIds { get; set; }
        public ICollection<ProductDTO> ProductDTOs { get; set; }
        public ICollection<FinancialAidDTO> FinancialAidDTOs { get; set; }
    }
}
