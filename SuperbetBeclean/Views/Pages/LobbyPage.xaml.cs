using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SuperbetBeclean.ViewModels;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    public partial class LobbyPage : Page
    {
        private Frame mainFrame;
        private MenuWindow mainWindow;
        private MainService service;
        private SqlConnection sqlConnection;
        private DataBaseService dbService;
        private string connectionString;
        private User user;
        public LobbyPage(Frame mainFrame, MenuWindow menuWindow, MainService service, User user)
        {
            connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            dbService = new DataBaseService(new SqlConnection(connectionString));
            InitializeComponent();
            this.mainFrame = mainFrame;
            mainWindow = menuWindow;
            this.service = service;
            this.user = user;
            PlayerNameTextBox.Text = menuWindow.UserName();
            PlayerLevelTextBox.Text = "Level: " + menuWindow.UserLevel().ToString();
            PlayerChipsTextBox.Text = "Chips: " + menuWindow.UserChips().ToString();
            if (!string.IsNullOrEmpty(user.UserCurrentIconPath))
            {
                PlayerIconImg.Source = new BitmapImage(new Uri(user.UserCurrentIconPath, UriKind.Absolute));
            }
            InternPlayerCount.Text = this.service.OccupiedIntern().ToString() + "/8";
            JuniorPlayerCount.Text = this.service.OccupiedJunior().ToString() + "/8";
            SeniorPlayerCount.Text = this.service.OccupiedSenior().ToString() + "/8";
        }

        private void ButtonLobbyBack(object sender, System.Windows.RoutedEventArgs e)
        {
            mainFrame.NavigationService.GoBack();
        }

        private void OnClickLeaderboardButton(object sender, System.Windows.RoutedEventArgs e)
        {
            List<string> strings;
            strings = dbService.GetLeaderboard();
            mainFrame.Navigate(new LeaderboardPage(mainFrame, strings));
        }
        public string ReturnUserNameOfLobbyPage()
        {
            return mainWindow.UserName();
        }
        private void OnShopButtonClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new ShopPage(mainFrame, mainWindow));
        }

        private void OnClickInternButton(object sender, System.Windows.RoutedEventArgs e)
        {
            int response = service.JoinInternTable(mainWindow);
            if (response == 1)
            {
                mainFrame.Navigate(mainWindow.InternPage());
            }
            else if (response == 0)
            {
                MessageBox.Show("Sorry, this table is full.");
            }
            else if (response == 1)
            {
                MessageBox.Show("Sorry, you don't have enough money.");
            }
        }

        private void OnClickJuniorBttn(object sender, System.Windows.RoutedEventArgs e)
        {
            int response = service.JoinJuniorTable(mainWindow);
            if (response == 1)
            {
                mainFrame.Navigate(mainWindow.JuniorPage());
            }
            else if (response == 0)
            {
                MessageBox.Show("Sorry, this table is full.");
            }
            else if (response == 1)
            {
                MessageBox.Show("Sorry, you don't have enough money.");
            }
        }

        private void OnClickSeniorButton(object sender, System.Windows.RoutedEventArgs e)
        {
            int response = service.JoinSeniorTable(mainWindow);
            if (response == 1)
            {
                mainFrame.Navigate(mainWindow.SeniorPage());
            }
            else if (response == 0)
            {
                MessageBox.Show("Sorry, this table is full.");
            }
            else if (response == 1)
            {
                MessageBox.Show("Sorry, you don't have enough money.");
            }
        }
        private void PlayerIconImg_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new ProfilePage(mainFrame, mainWindow));
        }

        private void ShopBttn_Click(object sender, RoutedEventArgs e)
        {
            string currentUserName = mainWindow.UserName();
            RequestsWindow requestWindow = new RequestsWindow(currentUserName, this, mainWindow.UserName());
            requestWindow.Show();
        }
    }
}