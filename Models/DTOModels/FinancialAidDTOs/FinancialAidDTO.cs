using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.ProductDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels.FinancialAidDTOs
{
    public class FinancialAidDTO
    {
        public int FinancialAidId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Minimum Balance")]
        public decimal MinBalance { get; set; } = decimal.Zero;
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Maximum Balance")]
        public decimal MaxBalance { get; set; } = decimal.Zero;
        [Required]
        public bool Percentage { get; set; }
        [Required]
        public decimal AidAmount { get; set; }
        public EstablishmentDTO EstablishmentDTO { get; set; }
        public ICollection<OrderDTO> OrderDTOs { get; set; }
        public ICollection<ProductTypeDTO> ProductTypeDTOs { get; set; }
    }
}
