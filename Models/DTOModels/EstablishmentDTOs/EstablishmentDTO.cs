using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Models.DTOModels.FinancialAidDTOs;
using Models.DTOModels.ProductDTOs;

namespace Models.DTOModels.EstablishmentDTOs
{
    public class EstablishmentDTO
    {
        public int EstablishmentID { get; set; }
        [Required(ErrorMessage = "Please enter the establishment name")]
        [Display(Name = "Establishment Name")]
        public string EstablishmentName { get; set; }
        [Required(ErrorMessage = "Please enter the establishment address")]
        [Display(Name = "Establishment Address")]
        public string EstablishmentAddress { get; set; }
        [Display(Name = "Establishment Description")]
        public string EstablishmentDescription { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; } = decimal.Zero;

        [DataType(DataType.ImageUrl)]
        [NotMapped]
        public ICollection<IFormFile> Images { get; set; }
        public EstablishmentTypeDTO EstablishmentTypeDTO { get; set; }        
        public ICollection<string> EstablishmentImageURLs { get; set; }
        public ICollection<ProductDTO> ProductDTOs { get; set; }
        public ICollection<FinancialAidDTO> FinancialAids { get; set; }
    }
}
