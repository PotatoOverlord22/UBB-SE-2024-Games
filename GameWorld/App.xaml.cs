using System.Windows;
using GameWorld;
using GameWorld.Entities;
using GameWorld.Services;
using GameWorld.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public partial class App : Application
    {
        private readonly IUserService userService;
        public App()
        {
            DependencyInjectionConfigurator.Init();
            userService = DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>();
            SetCurrentUser().Wait();

            MainMenu mainMenu = new MainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IMainMenuService>());
            mainMenu.Show();
        }
        private async Task SetCurrentUser()
        {
            User user = await userService.GetUserByIdAsync(Guid.Parse("19d3b857-9e75-4b0d-a0bc-cb945db12620"));
            GameStateManager.SetCurrentUser(user);
        }
    }
}
