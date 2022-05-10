using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.FinancialAidModels
{
    public class FinancialAid
    {
        [Key]
        public int FinancialAidId { get; set; }
        [Required]
        public decimal MinBalance { get; set; }
        [Required]
        public decimal MaxBalance { get; set; }

        [Required, ForeignKey("EstablishmentId")]
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
        public virtual ICollection<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
