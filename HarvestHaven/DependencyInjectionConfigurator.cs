using HarvestHaven.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public static class DependencyInjectionConfigurator
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureServices()
                .ConfigureCodeBehinds()
                .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }

    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAchievementService, AchievementService>();
            services.AddSingleton<IMarketService, MarketService>();
            services.AddSingleton<IFarmService, FarmService>();
            services.AddSingleton<ITradeService, TradeService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IResourceService, ResourceService>();
            services.AddSingleton<IMainMenuService, MainMenuService>();
            services.AddSingleton<IInventoryService, InventoryService>();

            return services;
        }

        public static IServiceCollection ConfigureCodeBehinds(this IServiceCollection services)
        {
            return services;
        }
    }
}
