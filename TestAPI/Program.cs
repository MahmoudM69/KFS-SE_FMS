using DataAccess;
using DataAccess.Models.UserModels;
using DataAccess.Services.IRepositories.IUserRepositories;
using DataAccess.Services.IRepositories;
using DataAccess.Services.Repository.UserRepositories;
using DataAccess.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add services to the container.

builder.Services.AddDbContextFactory<AppDbContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
{
    op.User.RequireUniqueEmail = true;
    op.Password.RequiredLength = 8;
    op.Password.RequireNonAlphanumeric = true;
    op.Password.RequireUppercase = true;
    op.Password.RequireLowercase = true;
    op.Password.RequireDigit = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IBaseModelRepository<>), typeof(BaseModelRepository<>));
builder.Services.AddScoped(typeof(IBaseUserRepository<>), typeof(BaseUserRepository<>));
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddServicesByTag();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
