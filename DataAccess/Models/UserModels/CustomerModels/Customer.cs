using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models.OrderModels;

namespace DataAccess.Models.UserModels.CustomerModels;

public class Customer : ApplicationUser
{
    [Required]
    public string Address { get; set; } = string.Empty;
    public virtual IEnumerable<Order>? Orders { get; set; }
}
