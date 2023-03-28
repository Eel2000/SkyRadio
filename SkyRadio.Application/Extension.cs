using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyRadio.Persistence;
using System.Reflection;

namespace SkyRadio.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(service => service.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddPersistenceLayer(configuration);

            return services;
        }
    }
}