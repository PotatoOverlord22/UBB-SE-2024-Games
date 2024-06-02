using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for OpponentPage.xaml
    /// </summary>
    public partial class OpponentPage : Page
    {
        public OpponentPage()
        {
            InitializeComponent();
        }

        private void HumanButton_Click(object sender, RoutedEventArgs e)
        {
            Router.OnlineGame = true;
            // this.NavigationService.Navigate(Router.LoadingPage);
        }

        private void RobotButton_Click(object sender, RoutedEventArgs e)
        {
            Router.OnlineGame = false;
            this.NavigationService.Navigate(Router.AiSelectionPage);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Router.MenuPage);
        }
    }
}
