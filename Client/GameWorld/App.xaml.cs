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
        private readonly IUserService userService;
        public App()
        {
            DependencyInjectionConfigurator.Init();
            userService = DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>();
            SetCurrentUser().Wait();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private async Task SetCurrentUser()
        {
            User user = new User(new Guid(), "test");
            try
            {
                user = await userService.GetUserByIdAsync(Guid.Parse(Constants.TEST_USER_ID));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                GameStateManager.SetCurrentUser(user);
            }
        }
    }
}
