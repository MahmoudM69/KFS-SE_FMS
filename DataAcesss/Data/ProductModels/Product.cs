using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.Shared;


namespace DataAcesss.Data.ProductModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
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

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Establishment_Product> Establishment_Products { get; set; }
    }
}
