using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcesss.Data.Shared
{
    public class ProductType_FinancialAid
    {
        public int Id { get; set; }
        [ForeignKey("ProductTypeId")]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        [ForeignKey("FinancialAidId")]
        public int FinancialAidId { get; set; }
        public virtual FinancialAid FinancialAid { get; set; }

        [Required]
        public bool Percentage { get; set; }
        [Required]
        public decimal AidAmount { get; set; }
    }
}
