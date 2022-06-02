using DataAcesss.Data.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IApplicationUserRepositories.ICustomerRepositories
{
    public interface ICustomerRepository
    {
        public Customer GetCustomerById(string id);
        public Customer GetCustomerByName(string Name);
    }
}
