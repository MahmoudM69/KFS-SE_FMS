using System;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.EstablishmentDTOs;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.UserDTOs.EmpolyeeDTOs;

public class EmployeeDTO : ApplicationUserDTO, IOwnableDTO
{
    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Working Since")]
    public DateTime WorkingSince { get; set; } = DateTime.Now;
    [Required]
    public int EstablishmentId { get; set; }
    public virtual EstablishmentDTO? EstablishmentDTO { get; set; }
}
