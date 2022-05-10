using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAcesss.Data.EstablishmentModels
{
    public class EstablishmentType
    {
        [Key]
        public int EstablishmentTypeId { get; set; }
        [Required]
        public string Type { get; set; }
        public virtual ICollection<Establishment> Establishments { get; set; }
    }
}
