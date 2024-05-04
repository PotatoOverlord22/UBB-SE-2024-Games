using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SetCurrentUser();

            Task.Delay(500).Wait();

            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }      
        
        private async void SetCurrentUser()
        {
            User user = await UserService.GetUserByIdAsync(Guid.Parse("19d3b857-9e75-4b0d-a0bc-cb945db12620"));
            GameStateManager.SetCurrentUser(user);
            await UserService.UpdateUserWater();
        }         
    }

}
