using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Application.Services;
using SkyRadio.Persistence;
using System.Reflection;

namespace SkyRadio.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(service => service.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ITokenProviderService, TokenProviderService>();

            services.AddPersistenceLayer(configuration);

            return services;
        }
    }
}