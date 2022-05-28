using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.ProductModels
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }
    }
}
