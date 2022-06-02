using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.EstablishmentModels;

namespace DataAcesss.Data.Shared
{
    public class Establishment_Product
    {
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("EstablishmentId")]
        [Required, Range(1, double.MaxValue, ErrorMessage = "Please choose an establishment")]
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
        public virtual List<Order> Orders { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        [Required, DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        [Required, Range(0, double.MaxValue, ErrorMessage = "This field must be a positive number")]
        public float Quantity { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "This field must be a positive number")]
        public decimal PurchasePrice { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "This field must be a positive number")]
        public decimal RetailPrice { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "This field must be a positive number")]
        public bool Percentage { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "This field must be a positive number")]
        public decimal AidAmount { get; set; }
        public decimal CalcTotal()
        {
            return (RetailPrice - (Percentage ? (RetailPrice * (AidAmount / 100)) : AidAmount));
        }

    }
}
