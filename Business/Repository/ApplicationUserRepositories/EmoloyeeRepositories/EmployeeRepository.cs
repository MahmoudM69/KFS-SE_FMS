using Business.IRepository.IApplicationUserRepositories.EmoloyeeIRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EmployeeModels;
using DataAcesss.Data.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.ApplicationUserRepositories.EmoloyeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;

        public EmployeeRepository(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
        public async Task<Employee> GetemployeeById(string id)
        {
            if(id != null)
            {
                Employee user = await context.Employees.Include(x => x.Establishment).FirstOrDefaultAsync(x => x.Id == id);
                //var user = await userManager.FindByIdAsync(id);
                if(user != null)
                {
                    return user;
                }
            }
            return null;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> users = context.Employees.ToList();
            if (users != null && users.Any())
            {
                return users;
            }
            return null;
        }

        public async void DeleteEmployee(string id)
        {
            if (id != null)
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
            }
        }
    }
}
