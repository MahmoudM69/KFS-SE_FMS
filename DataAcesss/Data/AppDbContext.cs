using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.EmployeeModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.PaymentModels;

namespace DataAcesss.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<EstablishmentImage> EstablishmentImages { get; set; }
        public DbSet<EstablishmentType> EstablishmentTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<FinancialAid> FinancialAids { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentService> PaymentServices { get; set; }
        public DbSet<Establishment_Product> Establishment_Products { get; set; }
        public DbSet<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().HasOne(c => c.Customer)
                                   .WithMany(co => co.Orders)
                                   .HasForeignKey(ci => ci.CustomerId);
            builder.Entity<Order>().HasOne(espr => espr.Establishment_Product)
                                   .WithMany(espro => espro.Orders)
                                   .HasForeignKey(espri => espri.Establishment_ProductId);
            builder.Entity<Order>().HasOne(f => f.FinancialAid)
                                   .WithMany(fo => fo.Orders)
                                   .HasForeignKey(fi => fi.FinancialAidId);
            builder.Entity<Order>().HasOne(p => p.Payment)
                                   .WithOne(po => po.Order)
                                   .HasForeignKey<Payment>(pi => pi.OrderId);

            builder.Entity<ProductType_FinancialAid>().HasOne(pt => pt.ProductType)
                                                   .WithMany(ptfa => ptfa.ProductType_FinancialAids)
                                                   .HasForeignKey(pti => pti.ProductTypeId);
            builder.Entity<ProductType_FinancialAid>().HasOne(fa => fa.FinancialAid)
                                                   .WithMany(ptfa => ptfa.ProductType_FinancialAids)
                                                   .HasForeignKey(fai => fai.FinancialAidId);

            builder.Entity<Establishment_Product>().HasOne(e => e.Establishment)
                                                   .WithMany(ep => ep.Establishment_Products)
                                                   .HasForeignKey(ei => ei.EstablishmentId);
            builder.Entity<Establishment_Product>().HasOne(p => p.Product)
                                                   .WithMany(ep => ep.Establishment_Products)
                                                   .HasForeignKey(pi => pi.ProductId);
        }

    }
}
