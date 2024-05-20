using System.Windows;
using System.Windows.Controls;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    public partial class MainMenu : Page
    {
        private Frame mainFrame;
        private MenuWindow menuWindow;
        private User user;
        private MainService service;
        public MainMenu(Frame mainFrame, MenuWindow mainWindow, MainService serv, User user)
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
            mainFrame.Navigate(new LobbyPage(mainFrame, menuWindow, service, user));
        }

        private void OnClickQuitButton(object sender, RoutedEventArgs e)
        {
            menuWindow.Close();
        }
    }
}
