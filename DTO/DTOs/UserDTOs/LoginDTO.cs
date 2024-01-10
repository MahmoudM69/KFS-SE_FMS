using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.UserDTOs;

public class LoginDTO
{
    [Required]
    [Display(Name = "User Name")]
    public string UseName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}
