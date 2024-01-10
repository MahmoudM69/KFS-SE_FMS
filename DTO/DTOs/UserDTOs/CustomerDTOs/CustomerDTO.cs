using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.OrderDTOs;

namespace DTO.DTOs.UserDTOs.CustomerDTOs;

public class CustomerDTO : ApplicationUserDTO
{
    [Required]
    public string? Address { get; set; }
    public ICollection<OrderDTO>? Orders { get; set; }
}
