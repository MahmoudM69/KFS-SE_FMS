using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.SharedDTOs.ApplicationUser;

namespace Models.DTOModels.CustomerDTOs
{
    public class CustomerDTO : ApplicationUserDTO
    {
        [Required]
        public string Address { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
