using System.ComponentModel.DataAnnotations;
using Models.DTOModels.CustomerDTOs;
using Models.DTOModels.FinancialAidDTOs;
using Models.DTOModels.PaymentDTOs;
using Models.DTOModels.SharedDTOs;

namespace Models.DTOModels.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Please enter the quantity")]
        public float Quantity { get; set; }
        public string Stats { get; set; } = "pending";
        [Required]
        public CustomerDTO CustomerDTO { get; set; }
        [Required]
        public Establishment_ProductDTO Establishment_ProductDTO { get; set; }
        public FinancialAidDTO FinancialAidDTO { get; set; }
        public PaymentDTO PaymentDTO { get; set; }
    }
}
