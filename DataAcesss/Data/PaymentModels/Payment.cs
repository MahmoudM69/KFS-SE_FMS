using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.OrderModels;

namespace DataAcesss.Data.PaymentModels
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        [Required]
        public string PaymentStatus { get; set; } = "pending"; // failed or confirmed
        
        [ForeignKey("PaymentServiceId")]
        public int PaymentServiceId { get; set; }
        public virtual PaymentService PaymentService { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
