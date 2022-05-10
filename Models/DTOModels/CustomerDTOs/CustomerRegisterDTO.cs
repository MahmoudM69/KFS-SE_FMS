using System.ComponentModel.DataAnnotations;
using Models.DTOModels.SharedDTOs.ApplicationUser;

namespace Models.DTOModels.CustomerDTOs
{
    public class CustomerRegisterDTO : RegisterDTO
    {
        [Required]
        public string Address { get; set; }
    }
}
