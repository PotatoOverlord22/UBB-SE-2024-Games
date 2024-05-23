using Server.API.Services;

namespace Server.API.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
