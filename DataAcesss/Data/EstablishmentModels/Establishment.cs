using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.EmployeeModels;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.EstablishmentModels
{
    public class Establishment
    {
        [Key]
        public int EstablishmentId { get; set; }
        [Required]
        public string EstablishmentName { get; set; }
        [Required]
        public string EstablishmentAddress { get; set; }
        public string EstablishmentDescription { get; set; }
        [Required]
        public decimal Balance { get; set; } = decimal.Zero;

        [ForeignKey("EstablishmentTypeId")]
        public int EstablishmentTypeId { get; set; }
        public virtual ICollection<EstablishmentImage> EstablishmentImages { get; set; }
        public virtual EstablishmentType EstablishmentType { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FinancialAid> FinancialAids { get; set; }
        public virtual ICollection<Establishment_Product> Establishment_Products { get; set; }
    }
}
