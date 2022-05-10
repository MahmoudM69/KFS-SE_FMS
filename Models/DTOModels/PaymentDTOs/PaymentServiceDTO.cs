using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOModels.PaymentDTOs
{
    public class PaymentServiceDTO
    {
        public int PaymentServiceId { get; set; }
        [Required]
        [Display(Name = "Payment Service Name")]
        public string PaymentServiceName { get; set; }
        [Required]
        [Display(Name = "Payment Service Provider")]
        public string PaymentServiceProvider { get; set; }
        [Required]
        [Display(Name = "Payment Service Fee")]
        public decimal PaymentServiceFee { get; set; }
        [Required]
        public bool Percentage { get; set; }
        public ICollection<PaymentDTO> PaymentDTOs { get; set; }
    }
}
