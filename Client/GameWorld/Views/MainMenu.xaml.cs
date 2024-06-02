using System.Windows;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayHarvestHavenButton_Click(object sender, RoutedEventArgs e)
        {
            HarvestHavenMainMenu harvestHavenMainMenu = new HarvestHavenMainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IHarvestHavenMainService>());
            harvestHavenMainMenu.Show();
            this.Close();
        }
    }
}
