using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOModels.EstablishmentDTOs
{
    public class EstablishmentDTO
    {
        public int EstablishmentID { get; set; }
        [Required(ErrorMessage = "Please enter the establishment name")]
        public string EstablishmentName { get; set; }
        [Required, DefaultValue(0)]
        public int EstablishmentType { get; set; }
        [Required(ErrorMessage = "Please enter the establishment address")]
        public string EstablishmentAddress { get; set; }
        public string EstablishmentDescription { get; set; }

        public virtual ICollection<EstablishmentImageDTO> EstablishmentImagesDTOs { get; set; }
        public ICollection<string> EstablishmentImageURLs { get; set; }
    }
}
