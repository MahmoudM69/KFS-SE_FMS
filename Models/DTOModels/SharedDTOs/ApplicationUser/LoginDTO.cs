using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels.SharedDTOs.ApplicationUser
{
    public class LoginDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UseName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
