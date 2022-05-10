using System;
using System.ComponentModel.DataAnnotations;
using Models.DTOModels.SharedDTOs.ApplicationUser;

namespace Models.DTOModels.EmpolyeeDTOs
{
    public class EmployeeDTO : ApplicationUserDTO
    {
        [Required]
        public DateTime WorkingSince { get; set; } = DateTime.Now;
        [Required]
        public int EstablishmentId { get; set; }
    }
}
