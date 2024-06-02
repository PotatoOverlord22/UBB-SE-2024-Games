using System.Windows;
using System.Windows.Controls;
using GameWorldClassLibrary.Models;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;
namespace GameWorld.Views
{
    public partial class PokerMainMenu : Page
    {
        private Frame mainFrame;
        private MenuWindow menuWindow;
        private User user;
        private ICasinoPokerMainService service;
        public PokerMainMenu(Frame mainFrame, MenuWindow mainWindow, ICasinoPokerMainService serv, User user)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            menuWindow = mainWindow;
            service = serv;
            this.user = user;
        }

        private void OnClickRulesButton(object sender, RoutedEventArgs e)
        {
            RulesWindow rulesWindow = new RulesWindow();
            rulesWindow.Show();
        }

        private void OnClickPlayButton(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new LobbyPage(mainFrame, menuWindow, service, user, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>()));
        }

        private void OnClickQuitButton(object sender, RoutedEventArgs e)
        {
            menuWindow.Close();
        }
    }
}
