using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAcesss.Data.PaymentModels
{
    public class PaymentService
    {
        [Key]
        public int PaymentServiceId { get; set; }
        [Required]
        public string PaymentServiceName { get; set; }
        [Required]
        public string PaymentServiceProvider { get; set; }
        [Required]
        public decimal PaymentServiceFee { get; set; }
        [Required]
        public bool Percentage { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
