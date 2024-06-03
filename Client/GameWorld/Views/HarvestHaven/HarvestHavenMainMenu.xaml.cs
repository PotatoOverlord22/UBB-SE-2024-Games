using System.Windows;
using System.Windows.Controls;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class HarvestHavenMainMenu : Page
    {
        public HarvestHavenMainMenu(IHarvestHavenMainService service)
        {
            InitializeComponent();
            DataContext = service;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Farm farmScreen = new Farm(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IFarmService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());
            NavigationService.Navigate(farmScreen);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }
}
