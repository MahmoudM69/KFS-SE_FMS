using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.EmployeeModels
{
    public class Employee : ApplicationUser
    {
        [Required]
        public DateTime WorkingSince { get; set; } = DateTime.Now;

        [Required ,ForeignKey("EstablishmentId")]
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
    }
}
