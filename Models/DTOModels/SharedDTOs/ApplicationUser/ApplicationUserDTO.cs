using System;
using System.Collections.Generic;

namespace Models.DTOModels.SharedDTOs.ApplicationUser
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public decimal Balance { get; set; }
        public decimal Salary { get; set; }
        public string Image { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
