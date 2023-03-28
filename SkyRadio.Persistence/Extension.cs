using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyRadio.Persistence.Contexts;

namespace SkyRadio.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SkyRadioDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Application"),
                    server => server.MigrationsAssembly(typeof(SkyRadioDbContext).Assembly.FullName));
            });

            services.AddDbContext<SkyRadioIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Identity"),
                    server => server.MigrationsAssembly(typeof(SkyRadioIdentityDbContext).Assembly.FullName));
            });


            return services;
        }
    }
}