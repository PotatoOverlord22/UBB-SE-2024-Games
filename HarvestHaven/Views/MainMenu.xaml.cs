using System.Windows;
using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            RefreshGUI();
        }

        private void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                GreetingLabel.Content = "Welcome, " + user.Username;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Farm farmScreen = new Farm(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IFarmService>());

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
