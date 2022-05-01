using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;

namespace DataAcesss.Data
{
    internal class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Establishment> Establishments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Establishment_Product>().HasOne(e => e.Establishment)
                                                   .WithMany(ep => ep.Establishment_Products)
                                                   .HasForeignKey(ei => ei.EstablishmentId);

            builder.Entity<Establishment_Product>().HasOne(p => p.Product)
                                                   .WithMany(ep => ep.Establishment_Products)
                                                   .HasForeignKey(pi => pi.ProductId);
        }

    }
}
