using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // this.NavigationService.Navigate(Router.ProfilePage);
        }

        private void ChessButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Chess";
            this.NavigationService.Navigate(Router.ChessSelectionPage);
        }

        private void Connect4Button_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Connect4";
            this.NavigationService.Navigate(Router.OpponentPage);
        }

        private void DartsButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Darts";
            this.NavigationService.Navigate(Router.OpponentPage);
        }

        private void ObstructionButton_Click(object sender, RoutedEventArgs e)
        {
            Router.GameType = "Obstruction";

            this.NavigationService.Navigate(Router.ObstructionModePage);
        }
    }
}