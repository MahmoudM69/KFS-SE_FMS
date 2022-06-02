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
        public int? PaymentServiceId { get; set; }
        public virtual PaymentService PaymentService { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public decimal Total { get; set; }
        public decimal CalcTotal()
        {
            decimal total = 0;
            //foreach(Order order in Orders)
            //{
            //    total = total + order.Total;
            //}
            total = total + Order.Total;
            total = total + (PaymentService != null ? PaymentService.PaymentServiceFee : 0);
            return total;
        }
    }
}
