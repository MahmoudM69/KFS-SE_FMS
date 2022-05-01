using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcesss.Data.EstablishmentModels
{
    public class EstablishmentImage
    {
        [Key]
        public int EstablishmentImageId { get; set; }
        public string EstablishmentImageUrl { get; set; }
        [ForeignKey("EstablishmentId")]
        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
    }
}
