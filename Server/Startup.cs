using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Common.Extensions;
using MudBlazor.Services;
using MudBlazor;
using Microsoft.Extensions.Hosting;
using DataAccess.Models.UserModels.CustomerModels;
using DataAccess.Services.Repository;
using DataAccess.Models.UserModels;
using DataAccess.Services.IRepositories.IUserRepositories;
using DataAccess.Services.Repository.UserRepositories;
using DataAccess.Services.IRepositories;
using DataAccess;
using DataAccess.Models.UserModels.EmployeeModels;
using DataAccess.Services;
using Business.IServices.IPaymentServices;
using Business.Services.PaymentServices;
using AutoMapper.Extensions.ExpressionMapping;
using Common.Classes;

namespace Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        services.AddHttpContextAccessor();
        
        services.AddDbContextFactory<AppDbContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        services.AddIdentityCore<Employee>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        services.AddIdentityCore<Customer>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        services.AddAutoMapper(cfg => cfg.AddExpressionMapping(), AppDomain.CurrentDomain.GetAssemblies());

        services.AddServicesByTag();

        services.AddScoped(typeof(IBaseModelRepository<>), typeof(BaseModelRepository<>));
        services.AddScoped(typeof(IBaseUserRepository<>), typeof(BaseUserRepository<>));
        services.AddScoped<IPaymentServiceDTOService, PaymentServiceDTOService>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<PageHistoryState, PageHistoryState>();
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddSingleton<WeatherForecastService>();
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 10000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.MaximumOpacity = 85;
            config.SnackbarConfiguration.BackgroundBlurred = true;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        services.AddScoped<IDBInitializer, DBInitializer>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDBInitializer dBInitializer)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //dBInitializer.Initialize();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
