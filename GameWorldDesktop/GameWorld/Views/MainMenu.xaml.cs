using System.Windows;
using System.Windows.Controls;
using GameWorld.Resources.Utils;
using GameWorld.Utils;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayHarvestHavenButton_Click(object sender, RoutedEventArgs e)
        {
           /* HarvestHavenMainMenu harvestHavenMainMenu = new HarvestHavenMainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IHarvestHavenMainService>());
            harvestHavenMainMenu.Show();
            this.Close();*/
           NavigationService.Navigate(new HarvestHavenMainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IHarvestHavenMainService>()));
        }

        private void SkillIssueBroButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkillIssueBroMainMenu());
        }

        private void ObstructionGameButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Obstruction";
            this.NavigationService.Navigate(Router.ObstructionModePage);
        }

        private void Connect4Button_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Connect4";
            this.NavigationService.Navigate(Router.OpponentPage);
        }
    }
}
