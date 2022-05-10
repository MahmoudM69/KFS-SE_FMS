using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels.EstablishmentDTOs
{
    public class EstablishmentTypeDTO
    {
        public int EstablishmentTypeId { get; set; }
        [Required(ErrorMessage = "Please enter an establishment Type")]
        public string Type { get; set; }
        public ICollection<EstablishmentDTO> EstablishmentDTOs { get; set; }
    }
}
