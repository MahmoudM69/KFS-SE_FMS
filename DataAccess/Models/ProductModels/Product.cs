using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models.SharedModels;
using Microsoft.EntityFrameworkCore;
using Common.Interfaces.Entities;

namespace DataAccess.Models.ProductModels;

[Index(nameof(Name))]
public class Product : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Required]
    public virtual IEnumerable<ProductType>? ProductTypes { get; set; }
    //public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
    public virtual IEnumerable<Establishment_Product>? Establishment_Products { get; set; }

    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
