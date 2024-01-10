using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Models.SharedModels;
using DataAccess.Models.UserModels.EmployeeModels;
using Common.Interfaces.Entities;

namespace DataAccess.Models.EstablishmentModels;

public class Establishment : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public virtual IEnumerable<EstablishmentType>? EstablishmentTypes { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public string LogoUrl { get; set; } = string.Empty;
    [Required]
    public double Balance { get; set; }
    public virtual IEnumerable<EstablishmentImage>? EstablishmentImages { get; set; }
    public virtual IEnumerable<Employee>? Employees { get; set; }
    public virtual IEnumerable<FinancialAid>? FinancialAids { get; set; }
    public virtual IEnumerable<Establishment_Product>? Establishment_Products { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
