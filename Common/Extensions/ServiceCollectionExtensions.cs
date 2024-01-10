using Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesByTag(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var servicesTypes = assemblies.SelectMany(x => x.GetTypes().Where(f => f.GetCustomAttribute<ServiceAttribute>() != null)).ToList();
        foreach (var interfaceType in servicesTypes.Where(x => x.IsInterface))
        {
            var implementationType = servicesTypes.FirstOrDefault(x => x.IsClass &&
                x.GetCustomAttribute<ServiceAttribute>()!.Type == interfaceType.GetCustomAttribute<ServiceAttribute>()!.Type);
            if (implementationType != null) services.AddScoped(interfaceType, implementationType);
        }
        return services;
    }
}
