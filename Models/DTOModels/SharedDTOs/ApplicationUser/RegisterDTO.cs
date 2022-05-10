using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Models.DTOModels.SharedDTOs.ApplicationUser
{
    public class RegisterDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.ImageUrl)]
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
