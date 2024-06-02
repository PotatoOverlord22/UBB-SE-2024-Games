using GameWorld.Repositories;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;
using GameWorld.Resources.Utils;

namespace GameWorld
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
            services.AddScoped<IAchievementService, AchievementService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IHarvestHavenMainService, HarvestHavenMainService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICasinoPokerMainService, CasinoPokerMainService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IDatabaseProvider, DatabaseProvider>();
            services.AddScoped<ITableService, TableService>();
            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseProvider, DatabaseProvider>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFarmCellRepository, FarmCellRepository>();
            services.AddScoped<IInventoryResourceRepository, InventoryResourceRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IMarketBuyItemRepository, MarketBuyItemRepository>();
            services.AddScoped<IMarketSellResourceRepository, MarketSellResourceRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
