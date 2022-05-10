using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter the product name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [DataType(DataType.ImageUrl)]
        [NotMapped]
        public ICollection<IFormFile> Images { get; set; }

        public ICollection<string> ProductImageURLs { get; set; }
    }
}
