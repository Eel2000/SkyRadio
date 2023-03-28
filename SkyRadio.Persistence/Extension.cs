using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyRadio.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

            services.AddDefaultIdentity<IdentityUser>()
               .AddEntityFrameworkStores<SkyRadioIdentityDbContext>()
               .AddDefaultTokenProviders();

            return services;
        }
    }
}