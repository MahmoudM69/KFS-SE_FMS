using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.EstablishmentModels
{
    public class Establishment
    {
        [Key]
        public int EstablishmentID { get; set; }
        [Required]
        public string EstablishmentName { get; set; }
        [Required, DefaultValue(0)]
        public int EstablishmentType { get; set; }
        [Required]
        public string EstablishmentAddress { get; set; }
        public string EstablishmentDescription { get; set; }

        public virtual ICollection<EstablishmentImage> EstablishmentImages { get; set; }
        public virtual ICollection<Establishment_Product> Establishment_Products { get; set; }
    }
}
