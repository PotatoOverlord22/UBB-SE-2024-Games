using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace Server.API.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IAchievementRepository, AchievementRepositoryDB>();
            services.AddScoped<IItemRepository, ItemRepositoryDB>();
            services.AddScoped<IResourceRepository, ResourceRepositoryDB>();
            services.AddScoped<IInventoryResourceRepository, InventoryResourceRepositoryDB>();
            services.AddScoped<IMarketBuyItemRepository, MarketBuyItemRepositoryDB>();
            services.AddScoped<IUserRepository, UserRepositoryDB>();
            services.AddScoped<IFarmCellRepository, FarmCellRepositoryDB>();
            services.AddScoped<ITradeRepository, TradeRepositoryDB>();
            services.AddScoped<IMarketSellResourceRepository, MarketSellResourceRepositoryDB>();
            services.AddScoped<ICommentRepository, CommentRepositoryDB>();
        }

        public static void ConfigureContexts(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<GamesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GamesContext"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Server.API")));
        }
    }
}