using Common.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.UserModels;

public class ApplicationUser : IdentityUser, ISoftDeletableEntity
{
    [Required]
    public DateTime DOB { get; set; }
    public double Balance { get; set; } = 0;
    public double Salary { get; set; } = 0;
    public string AvatarUrl { get; set; } = string.Empty;

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
