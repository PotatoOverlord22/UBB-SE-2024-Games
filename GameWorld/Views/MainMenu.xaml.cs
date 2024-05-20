using System.Windows;
using GameWorld;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public partial class MainMenu : Window
    {
        public MainMenu(IMainMenuService service)
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
