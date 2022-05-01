using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOModel.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter the product name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter the product serial number")]
        public long SerialNumber { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        [Required]
        public decimal Price { get; set; } = decimal.Zero;
        [Required]
        public decimal OfferPercentage { get; set; } = decimal.Zero;
        [Required, DefaultValue(1)]
        public float Quantity { get; set; }
        public virtual ICollection<ProductImageDTO> ProductImageDTOs { get; set; }
        public ICollection<string> ProductImageURLs { get; set; }
    }
}
