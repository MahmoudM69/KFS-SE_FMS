using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcesss.Data.Shared
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime DOB { get; set; }
        public decimal Balance { get; set; } = 0;
        public decimal Salary { get; set; } = 0;
        public string ImageUrl { get; set; }
    }
}
