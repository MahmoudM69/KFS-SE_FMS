using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.UserDTOs;

public class RegisterDTO : ApplicationUserDTO
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
    [DataType(DataType.Password), Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
