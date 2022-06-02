using DataAcesss.Data;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.EmployeeModels;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Service
{
    public class DBInitializer : IDBInitializer
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DBInitializer(AppDbContext context, 
               UserManager<ApplicationUser> userManager, 
               RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Initialize()
        {
            //if(context.Database.GetPendingMigrations().Count() > 0)
            //{
            //    context.Database.Migrate();
            //}

            string[] Roles = new string[4] { "Admin", "Manager", "Employee", "Customer" };
            if (Roles.All(r => context.Roles.Any(db => db.Name == r)))
                return;

            EstablishmentType type = context.EstablishmentTypes.Add(new EstablishmentType
            {
                Type = "Establishment Type Test"
            }).Entity;

            Establishment establishment = context.Establishments.Add(new Establishment
            {
                EstablishmentName = "Establishment Test",
                EstablishmentDescription = "Establishment Description Test",
                EstablishmentAddress = "Establishment Address Test",
                Balance = 6969,
                EstablishmentType = type
            }).Entity;

            ApplicationUser user = new();
            IdentityResult result = new();
            result = userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            }, "Admin1***").GetAwaiter().GetResult();
            user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
            roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

            result = userManager.CreateAsync(new Employee
            {
                UserName = "manager@manager.com",
                Email = "manager@manager.com",
                EmailConfirmed = true,
                Establishment = establishment
            }, "Manager1***").GetAwaiter().GetResult();
            user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "manager@manager.com");
            roleManager.CreateAsync(new IdentityRole("Manager")).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Manager").GetAwaiter().GetResult();

            result = userManager.CreateAsync(new Employee
            {
                UserName = "employee@employee.com",
                Email = "employee@employee.com",
                EmailConfirmed = true,
                Establishment = establishment
            }, "Employee1***").GetAwaiter().GetResult();
            user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "employee@employee.com");
            roleManager.CreateAsync(new IdentityRole("Employee")).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Employee").GetAwaiter().GetResult();

            result = userManager.CreateAsync(new Customer
            {
                UserName = "customer@customer.com",
                Email = "customer@customer.com",
                EmailConfirmed = true
            }, "Customer1***").GetAwaiter().GetResult();
            user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "customer@customer.com");
            roleManager.CreateAsync(new IdentityRole("Customer")).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Customer").GetAwaiter().GetResult();
        }
    }
}
