using GameWorldWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace GameWorldWeb.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureContexts(IServiceCollection services, ConfigurationManager configuration)
        {
            // Add required contexts to the DI Container
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
        }
        public static void ConfigureRepositories(IServiceCollection services)
        {
            // Add required repositories to the DI Container

        }
        public static void ConfigureServices(IServiceCollection services)
        {
            // Add required services to the DI Container

        }
    }
}