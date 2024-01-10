using Common.Enums;
using Common.Extensions;
using Common.Interfaces.Entities;
using DataAccess.Models.UserModels.CustomerModels;
using DataAccess.Models.SharedModels;
using DataAccess.Models.OrderModels;
using DataAccess.Models.UserModels;
using DataAccess.Models.ProductModels;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Models.PaymentModels;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Models.UserModels.EmployeeModels;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly int? EstablishmentId = null;
    public bool FilterSoftDelete { get; set; } = true;

    //private static Dictionary<Type, IMutableEntityType>? _ownableEntityTypes;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {

        EstablishmentId = int.TryParse(httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "EstablishmentId")?.Value, out int establishmentId) ? establishmentId : null;
        //ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //if (_ownableEntityTypes == null)
        //{
        //    // Initialize the dictionary or cache only once
        //    _ownableEntityTypes = builder.Model.GetEntityTypes().Where(entityType => typeof(IOwnableEntity).IsAssignableFrom(entityType.ClrType)).ToDictionary(et => et.ClrType);
        //}

        //builder.Entity<Establishment>()
        //       .HasMany(e => e.EstablishmentTypes)
        //       .WithMany(et => et.Establishments)
        //       .UsingEntity(t => t.ToTable("EstablishmentEstablishmentType"));

        //builder.Entity<EstablishmentType>().ToTable(nameof(EstablishmentType).Pluralize()).HasBaseType<BaseType>().Property(x => x.Id);
        //builder.Entity<EstablishmentImage>().ToTable(nameof(EstablishmentImage).Pluralize()).HasBaseType<BaseImage>().Property(x => x.Id);
        //builder.Entity<ProductImage>().ToTable(nameof(ProductImage).Pluralize()).HasBaseType<BaseImage>().Property(x => x.Id);
        //builder.Entity<ProductType>().ToTable(nameof(ProductType).Pluralize()).HasBaseType<BaseType>().Property(x => x.Id);

        builder.Entity<Employee>().ToTable(nameof(Employee).Pluralize()).HasBaseType<ApplicationUser>().Property(x => x.Id);
        builder.Entity<Customer>().ToTable(nameof(Customer).Pluralize()).HasBaseType<ApplicationUser>().Property(x => x.Id);

        builder.Entity<Order>()
            .Property(e => e.Status)
            .HasConversion(v => v.ToString(), v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
        builder.Entity<Payment>()
            .Property(e => e.Status)
            .HasConversion(v => v.ToString(), v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));

        builder.Model.GetEntityTypes()
            .Where(entityType => typeof(IOwnableEntity).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null).ToList()
            .ForEach(entityType =>
            {
                builder.Entity(entityType.ClrType).HasQueryFilter<IOwnableEntity>(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
            });
        builder.Model.GetEntityTypes()
            .Where(entityType => typeof(ISoftDeletableEntity).IsAssignableFrom(entityType.ClrType) && entityType.BaseType == null).ToList()
            .ForEach(entityType =>
            {
                builder.Entity(entityType.ClrType).HasQueryFilter<ISoftDeletableEntity>(x => !FilterSoftDelete || x.SoftDelete >= 0);
            });
        //builder.Entity<Employee>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //builder.Entity<Establishment>().ToTable(nameof(Establishment).Pluralize()).HasBaseType<BaseEntity>().Property(x => x.Id);
        //builder.Entity<Product>().ToTable(nameof(Product).Pluralize()).HasBaseType<BaseEntity>().Property(x => x.Id);
        //builder.Entity<FinancialAid>().ToTable(nameof(FinancialAid).Pluralize()).HasBaseType<OwnableEntity>().Property(x => x.Id);
        //builder.Entity<Order>().ToTable(nameof(Order).Pluralize()).HasBaseType<OwnableEntity>().Property(x => x.Id);
        //builder.Entity<Payment>().ToTable(nameof(Payment).Pluralize()).HasBaseType<OwnableEntity>().Property(x => x.Id);
        //builder.Entity<PaymentService>().ToTable(nameof(PaymentService).Pluralize()).HasBaseType<BaseEntity>().Property(x => x.Id);
        //builder.Entity<Establishment_Product>().ToTable(nameof(Establishment_Product).Pluralize()).HasBaseType<OwnableEntity>().Property(x => x.Id);
        //builder.Entity<ProductType_FinancialAid>().ToTable(nameof(ProductType_FinancialAid).Pluralize()).HasBaseType<OwnableEntity>().Property(x => x.Id);

        //builder.Model.GetEntityTypes().Where(entityType => typeof(IOwnableEntity).IsAssignableFrom(entityType.ClrType)).ToList().ForEach(entityType =>
        //{
        //    var parameter = Expression.Parameter(entityType.ClrType);
        //    Expression<Func<IOwnableEntity, bool>> expression = (x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //    var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameter, expression.Body);
        //    builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newBody, parameter));
        //});

        //builder.Entity<ApplicationUser>().HasQueryFilter(x => !FilterSoftDelete || x.SoftDelete >= 0);
        //builder.Entity<Employee>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //builder.Entity<BaseEntity>().HasQueryFilter(x => !FilterSoftDelete || x.SoftDelete >= 0);
        //builder.Entity<OwnableEntity>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //builder.Entity<OwnableImage>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);

        //builder.Entity<SoftDeletableEntity>().HasQueryFilter(x => !FilterSoftDelete || x.SoftDelete >= 0);
        //builder.Entity<OwnableEntity>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //builder.Entity<OwnableImage>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
        //builder.Entity<IOwnableEntity>().HasQueryFilter(x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<EstablishmentImage> EstablishmentImages { get; set; }
    public DbSet<EstablishmentType> EstablishmentTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentService> PaymentServices { get; set; }
    public DbSet<FinancialAid> FinancialAids { get; set; }
    public DbSet<Establishment_Product> Establishment_Products { get; set; }
    public DbSet<ProductType_FinancialAid> ProductType_FinancialAids { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker.Entries().Where(er => er.Entity is ISoftDeletableEntity)
                                              .Select(er => er.Entity as ISoftDeletableEntity);
        bool isRecover = entities.Any(e => e != null && e.SoftDelete > 0);
        foreach (var entry in ChangeTracker.Entries().Where(er => er.State == EntityState.Deleted && er.Entity is ISoftDeletableEntity))
        {
            if (!((ISoftDeletableEntity)entry.Entity).IsRecoverable) continue;
            int status = ((ISoftDeletableEntity)entry.Entity).SoftDelete;
            status = isRecover ? ++status : --status;
            if (status > 0) status = 0;
            ((ISoftDeletableEntity)entry.Entity).SoftDelete = status;
            entry.State = EntityState.Modified;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
