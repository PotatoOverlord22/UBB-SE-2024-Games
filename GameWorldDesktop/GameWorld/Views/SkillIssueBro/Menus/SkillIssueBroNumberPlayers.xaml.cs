using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class SkillIssueBroNumberPlayers : Page
    {
        public SkillIssueBroNumberPlayers()
        {
            InitializeComponent();
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkillIssueBroMainMenu());
        }

        private void OnChooseTwoPlayers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<GameBoardWindow>());
        }

        private void OnChooseThreePlayers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<GameBoardWindow>());
        }

        private void OnChooseFourPlayers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<GameBoardWindow>());
        }
    }
}
