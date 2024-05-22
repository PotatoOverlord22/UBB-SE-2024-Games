using System.Windows;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;
using GameWorld.Views;
using GameWorld.Resources.Utils;
using GameWorld.Models;

namespace GameWorld
{
    public partial class App : Application
    {
       /* private readonly IUserService userService;*/
        public App()
        {
            DependencyInjectionConfigurator.Init();
            /*userService = DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>();
            MainMenu mainMenu = new MainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IHarvestHavenMainMenuService>());
            mainMenu.Show();*/
            SetCurrentUser().Wait();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private async Task SetCurrentUser()
        {
            /*User user = await userService.GetUserByIdAsync(Guid.Parse("19d3b857-9e75-4b0d-a0bc-cb945db12620"));*/
            User user = new User(Guid.NewGuid(), "test");
            GameStateManager.SetCurrentUser(user);
        }
    }
}
