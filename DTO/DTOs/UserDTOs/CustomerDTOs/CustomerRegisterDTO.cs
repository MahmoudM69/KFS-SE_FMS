using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.UserDTOs.CustomerDTOs;

public class CustomerRegisterDTO : RegisterDTO
{
    [Required]
    public string? Address { get; set; }
}
