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
        public ICollection<ProductDTO> ProductDTOIds { get; set; }
    }
}
