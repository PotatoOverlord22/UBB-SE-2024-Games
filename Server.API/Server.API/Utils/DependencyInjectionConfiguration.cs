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
            services.AddScoped<IInventoryResourceService, InventoryResourceService>();
            services.AddScoped<IMarketBuyItemService, MarketBuyItemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFarmCellService, FarmCellService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPlayingCardService, PlayingCardService>();
            services.AddScoped<IIconService, IconService>();
            services.AddScoped<IFontService, FontService>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<IShopItemService, ShopItemService>();
            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IChallengeService, ChallengeService>();
        }
    }
}