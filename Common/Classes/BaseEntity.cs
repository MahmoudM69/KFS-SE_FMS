using Common.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace Common.Classes;

public class BaseEntity : IBaseEntity
{
    [Key]
    public int Id { get; set; }
}
