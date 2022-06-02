using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.PaymentModels;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.OrderModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public float Quantity { get; set; }
        [Required]
        public string Stats { get; set; } = "pending";

        [Required, ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Required, ForeignKey("Establishment_ProductId")]
        public int Establishment_ProductId { get; set; }
        public virtual Establishment_Product Establishment_Product { get; set; }
        [ForeignKey("FinancialAidId")]
        public int? FinancialAidId { get; set; }
        public virtual FinancialAid FinancialAid { get; set; }
        [ForeignKey("PaymentId")]
        public int? PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public decimal Total { get; set; }
        //public decimal CalcTotal()
        //{
        //    decimal total = establishment_Product.CalcTotal();
        //    total = total / (decimal)(establishment_Product.Quantity / Quantity);
        //    total = total - (FinancialAid != null ? (FinancialAid.Percentage ? (total * (FinancialAid.AidAmount / 100)) : FinancialAid.AidAmount) : 0);
        //    total = total + (Payment != null ? Payment.PaymentService.PaymentServiceFee : 0);
        //    return total;
        //}
    }
}
