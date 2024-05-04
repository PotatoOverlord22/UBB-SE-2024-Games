using System.Windows;
using HarvestHaven.Entities;
using HarvestHaven.Utils;

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
            Farm farmScreen = new Farm();

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
