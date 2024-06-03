using System.Net.Http;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Resources.Utils
{
    public static class DependencyInjectionConfigurator
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureRepositories()
                .ConfigureServices()
                .BuildServiceProvider();
            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }

    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IHarvestHavenMainService, HarvestHavenMainService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IDatabaseProvider, DatabaseProvider>();
            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseProvider, DatabaseProvider>();
            services.AddScoped<IAchievementRepository, AchievementRepositoryHttp>();
            services.AddScoped<ICommentRepository, CommentRepositoryHttp>();
            services.AddScoped<IFarmCellRepository, FarmCellRepositoryHttp>();
            services.AddScoped<IInventoryResourceRepository, InventoryResourceRepositoryHttp>();
            services.AddScoped<IItemRepository, ItemRepositoryHttp>();
            services.AddScoped<IMarketBuyItemRepository, MarketBuyItemRepositoryHttp>();
            services.AddScoped<IMarketSellResourceRepository, MarketSellResourceRepositoryHttp>();
            services.AddScoped<IResourceRepository, ResourceRepositoryHttp>();
            services.AddScoped<ITradeRepository, TradeRepositoryHttp>();
            services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();
            services.AddScoped<IUserRepository, UserRepositoryHttp>();

            return services;
        }
    }
}
