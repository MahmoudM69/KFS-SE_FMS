using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.Shared;


namespace DataAcesss.Data.ProductModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [ForeignKey("ProductTypeId")]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Establishment_Product> Establishment_Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
