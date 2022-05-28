using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data.CustomerModels
{
    public class Customer : ApplicationUser
    {
        [Required]
        public string Address { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
