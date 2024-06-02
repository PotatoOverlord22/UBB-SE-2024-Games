using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GameWorld.Utils;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for ObstructionGameMode.xaml
    /// </summary>
    public partial class ObstructionGameMode : Page
    {
        private string gameMode;
        public ObstructionGameMode()
        {
            InitializeComponent();
        }

        private void SmallButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ObstructionMode = 5;
            NavigationService.Navigate(Router.OpponentPage);
        }

        private void MediumButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ObstructionMode = 8;
            NavigationService.Navigate(Router.OpponentPage);
        }

        private void LargeButton_Click(object sender, RoutedEventArgs e)
        {
            Router.ObstructionMode = 12;
            NavigationService.Navigate(Router.OpponentPage);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
