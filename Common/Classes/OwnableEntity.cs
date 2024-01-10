using Common.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace Common.Classes;

public class OwnableEntity : IOwnableEntity, ISoftDeletableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EstablishmentId { get; set; }

    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
