using DataAcesss.Data.EmployeeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IApplicationUserRepositories.EmoloyeeIRepositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee> GetemployeeById(string id);
        public List<Employee> GetEmployees();
        public void DeleteEmployee(string id);
    }
}
