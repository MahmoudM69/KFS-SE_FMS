using Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Shared.Classes;

public class BaseEntity : IBaseEntity, ISoftDeletable
{
    [Key]
    public int Id { get; set; }
    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
