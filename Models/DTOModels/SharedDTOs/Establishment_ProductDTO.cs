using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.EstablishmentDTOs;

namespace Models.DTOModels.SharedDTOs
{
    public class Establishment_ProductDTO
    {
        public int Id { get; set; }
        public int ProductDTOId { get; set; }
        public virtual ProductDTO ProductDTO { get; set; }
        public int EstablishmentDTOId { get; set; }
        public virtual EstablishmentDTO EstablishmentDTO { get; set; }
        public virtual ICollection<OrderDTO> OrderDTOs { get; set; }
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
