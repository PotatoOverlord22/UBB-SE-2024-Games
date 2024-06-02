using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for SkillIssueBroMainMenu.xaml
    /// </summary>
    public partial class SkillIssueBroMainMenu : Page
    {
        public SkillIssueBroMainMenu()
        {
            InitializeComponent();
        }

        private void OnHostButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkillIssueBroNumberPlayers());
        }

        private void OnJoinButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkillIssueBroJoinWithCode());
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }
}
