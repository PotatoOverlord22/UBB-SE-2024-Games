using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for SkillIssueBroJoinWithCode.xaml
    /// </summary>
    public partial class SkillIssueBroJoinWithCode : Page
    {
        public SkillIssueBroJoinWithCode()
        {
            InitializeComponent();
        }

        private void OnClickJoin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameBoardWindow());
        }
    }
}
