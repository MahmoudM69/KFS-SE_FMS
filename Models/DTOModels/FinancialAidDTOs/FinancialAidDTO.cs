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
        public EstablishmentDTO Establishment { get; set; }
        public ICollection<ProductTypeDTO> ProductTypes { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
