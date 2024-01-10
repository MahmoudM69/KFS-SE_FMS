using Common.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.ProductModels;

[Index(nameof(ImageUrl))]
public class ProductImage : IBaseEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Required]
    public int ProductId { get; set; }
    [Required, ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
