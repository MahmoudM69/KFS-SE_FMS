using System.ComponentModel.DataAnnotations;
using Models.DTOModels.CustomerDTOs;
using Models.DTOModels.FinancialAidDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.PaymentDTOs;

namespace Models.DTOModels.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Please enter the quantity")]
        public float Quantity { get; set; }
        [Required]
        public CustomerDTO CustomerDTO { get; set; }
        [Required]
        public ProductDTO ProductDTO { get; set; }
        public FinancialAidDTO FinancialAidDTO { get; set; }
        public PaymentDTO PaymentDTO { get; set; }
    }
}
