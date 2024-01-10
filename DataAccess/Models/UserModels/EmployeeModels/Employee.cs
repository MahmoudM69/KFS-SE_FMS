using Common.Interfaces.Entities;
using DataAccess.Models.EstablishmentModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.UserModels.EmployeeModels;

public class Employee : ApplicationUser, IOwnableEntity
{
    [Required]
    public DateTime WorkingSince { get; set; } = DateTime.Now;

    [Required]
    public int EstablishmentId { get; set; }
    [Required, ForeignKey(nameof(EstablishmentId))]
    public virtual Establishment? Establishment { get; set; }
}
