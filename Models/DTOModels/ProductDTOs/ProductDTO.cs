using Microsoft.AspNetCore.Http;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.SharedDTOs;
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

        public List<string> ProductImageURLs { get; set; }
        public ICollection<ProductImageDTO> ProductImageDTOs { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeDTO ProductTypeDTO { get; set; }
        public ICollection<Establishment_ProductDTO> Establishment_ProductDTOs { get; set; }
        public ICollection<OrderDTO> OrderDTOs { get; set; }

    }
}
