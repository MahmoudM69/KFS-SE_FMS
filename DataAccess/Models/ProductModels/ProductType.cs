using Common.Interfaces.Entities;
using DataAccess.Models.SharedModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.ProductModels;

[Index(nameof(Type))]
public class ProductType : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual IEnumerable<Product>? Products { get; set; }
    public virtual IEnumerable<ProductType_FinancialAid>? ProductType_FinancialAids { get; set; }

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
