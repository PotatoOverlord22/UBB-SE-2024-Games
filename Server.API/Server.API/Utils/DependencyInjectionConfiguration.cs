using Server.API.Services;

namespace Server.API.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAchievementService, AchievementService>();
        }
    }
}
