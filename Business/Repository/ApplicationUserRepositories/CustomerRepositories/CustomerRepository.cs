using Business.IRepository.IApplicationUserRepositories.ICustomerRepositories;
using DataAcesss.Data;
using DataAcesss.Data.CustomerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.ApplicationUserRepositories.CustomerRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Customer GetCustomerById(string id)
        {
            if(id != null) 
            {
                return context.Customers.Include(x => x.Orders).FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public Customer GetCustomerByName(string Name)
        {
            if (Name != null)
            {
                return context.Customers.Include(x => x.Orders).FirstOrDefault(x => x.UserName == Name);
            }
            return null;
        }
    }
}
