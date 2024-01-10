using Microsoft.AspNetCore.Http;
using Common.Interfaces;
using Common.Interfaces.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.UserDTOs;

public class ApplicationUserDTO : ISoftDeletableDTO, IValidatable
{
    public string? Id { get; set; }

    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber), Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date), Display(Name = "Date of Birth")]
    public DateTime DOB { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }

    public string AvatarUrl { get; set; } = string.Empty;
    [DataType(DataType.ImageUrl)]
    public IFormFile? Avatar { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
