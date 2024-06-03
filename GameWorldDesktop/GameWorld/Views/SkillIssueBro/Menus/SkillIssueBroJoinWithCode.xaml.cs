using System.Windows;
using System.Windows.Controls;
using GameWorld.Resources.Utils;
using Microsoft.Extensions.DependencyInjection;

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
            NavigationService.Navigate(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<GameBoardWindow>());
        }
    }
}
