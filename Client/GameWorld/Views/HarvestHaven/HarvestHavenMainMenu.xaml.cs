using System.Windows;
using GameWorld.Resources.Utils;
using GameWorld.Services;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class HarvestHavenMainMenu : Window
    {
        public HarvestHavenMainMenu(IHarvestHavenMainService service)
        {
            InitializeComponent();
            DataContext = service;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Farm farmScreen = new Farm(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IFarmService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());

            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
