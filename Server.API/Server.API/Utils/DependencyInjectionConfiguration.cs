using Server.API.Repositories;

namespace Server.API.Utils
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<GameWorldClassLibrary.Repositories.IAchievementRepository, GameWorldClassLibrary.Repositories.AchievementRepositoryDB>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IInventoryResourceRepository, InventoryResourceRepository>();
            services.AddScoped<IMarketBuyItemRepository, MarketBuyItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFarmCellRepository, FarmCellRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPlayingCardRepository, PlayingCardRepository>();
            services.AddScoped<IIconRepository, IconRepository>();
            services.AddScoped<IFontRepository, FontRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IShopItemRepository, ShopItemRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IChallengeRepository, ChallengeRepository>();
            services.AddScoped<IMarketSellResourceRepository, MarketSellResourceRepository>();
        }
    }
}