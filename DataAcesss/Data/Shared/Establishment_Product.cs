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
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
        public virtual List<Order> Orders { get; set; }
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        [Required]
        public float Quantity { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public decimal RetailPrice { get; set; }
        [Required]
        public bool Percentage { get; set; }
        [Required]
        public decimal AidAmount { get; set; }
    }
}
