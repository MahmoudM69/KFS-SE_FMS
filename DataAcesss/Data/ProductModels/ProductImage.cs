using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcesss.Data.ProductModels
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }
        public string ProductImageUrl { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
