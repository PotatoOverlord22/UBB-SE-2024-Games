using GameWorldClassLibrary.Repositories.Interfaces;
using Server.API.Repositories;

namespace Server.API.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAchievementRepository, GameWorldClassLibrary.Repositories.AchievementRepositoryDB>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IInventoryResourceRepository, InventoryResourceRepository>();
            services.AddScoped<IMarketBuyItemRepository, MarketBuyItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFarmCellRepository, FarmCellRepositoryDB>();
            services.AddScoped<ICommentRepository, CommentRepositoryDB>();
            services.AddScoped<IPlayingCardRepository, PlayingCardRepository>();
            services.AddScoped<IIconRepository, IconRepository>();
            services.AddScoped<IFontRepository, FontRepositoryDB>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IShopItemRepository, ShopItemRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IChallengeRepository, ChallengeRepositoryDB>();
            services.AddScoped<IMarketSellResourceRepository, MarketSellResourceRepository>();
        }
    }
}