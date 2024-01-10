using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.DTOs.FinancialAidDTOs;
using DTO.DTOs.SharedDTOs;
using DTO.DTOs.UserDTOs.EmpolyeeDTOs;
using Microsoft.AspNetCore.Http;
using Common.Interfaces;
using Common.Interfaces.DTOs;

namespace DTO.DTOs.EstablishmentDTOs;

public class EstablishmentDTO : IBaseDTO, ISoftDeletableDTO, IValidatable
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the establishment name")]
    [Display(Name = "Establishment Name")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please enter the establishment address")]
    [Display(Name = "Establishment Address")]
    public string? Address { get; set; }
    [Display(Name = "Establishment Description")]
    public string? Description { get; set; }
    [DataType(DataType.ImageUrl)]
    public IFormFile? Logo { get; set; }
    [Required]
    public string? LogoUrl { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public double Balance { get; set; }

    [Required]
    public IEnumerable<EstablishmentTypeDTO>? EstablishmentTypeDTOs { get; set; }
    public IEnumerable<EstablishmentImageDTO>? EstablishmentImageDTOs { get; set; }
    public IEnumerable<EmployeeDTO>? EmployeeDTOs { get; set; }
    public IEnumerable<FinancialAidDTO>? FinancialAidDTOs { get; set; }
    public IEnumerable<Establishment_ProductDTO>? Establishment_ProductDTOs { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
