using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SuperbetBeclean.Pages;
using SuperbetBeclean.Services;

namespace SuperbetBeclean
{
    public partial class RequestsWindow : Window
    {
        private string currentUserName;
        private List<string> requests;
        private SqlConnection sqlConnection;
        private IDataBaseService dbService;
        private string connectionString;
        private LobbyPage lobbyPage;
        public string UserName;
        public RequestsWindow(string currentUserName, LobbyPage lobbyPage, string userName)
        {
            InitializeComponent();
            UserName = userName;
            connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            dbService = new DataBaseService(new SqlConnection(connectionString)); // Initialize the database service
            this.currentUserName = currentUserName;
            this.lobbyPage = lobbyPage;
            // Call a method to load and display requests
            LoadRequests();
            chipsInRequestPage.Text = dbService.GetChipsByUserId(dbService.GetUserIdByUserName(currentUserName)).ToString();
        }

        private void LoadRequests()
        {
            requests = dbService.GetAllRequestsByToUserID(dbService.GetUserIdByUserName(currentUserName)); // Get requests from the database
            RequestsStackPanel.Children.Clear();
            // Create and add request items dynamically
            foreach (string requestInfo in requests)
            {
                Border requestBorder = new Border();
                requestBorder.CornerRadius = new CornerRadius(5);
                requestBorder.Background = Brushes.White;
                requestBorder.Margin = new Thickness(5);

                StackPanel requestPanel = new StackPanel();
                requestPanel.Orientation = Orientation.Horizontal;
                requestPanel.HorizontalAlignment = HorizontalAlignment.Stretch;

                TextBlock requestTextBlock = new TextBlock();
                requestTextBlock.Text = requestInfo;
                requestTextBlock.Margin = new Thickness(10);
                requestTextBlock.VerticalAlignment = VerticalAlignment.Center;
                requestPanel.Children.Add(requestTextBlock);
                requestBorder.Child = requestPanel;
                RequestsStackPanel.Children.Add(requestBorder);
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle accept button click event
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle decline button click event
        }
        // Accept all
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Tuple<int, int>> requests = dbService.GetAllRequestsByToUserIDSimplified(dbService.GetUserIdByUserName(currentUserName));

            foreach (Tuple<int, int> request in requests)
            {
                int fromUserID = request.Item1;
                int toUserID = request.Item2;
                int numberChips = dbService.GetChipsByUserId(fromUserID) + 3000;
                dbService.UpdateUserChips(fromUserID, dbService.GetChipsByUserId(fromUserID) + 3000);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(RequestsWindow))
                    {
                        RequestsWindow requestWindow = (RequestsWindow)window;
                        if (requestWindow.UserName == dbService.GetUserNameByUserId(fromUserID))
                        {
                            // _lobbyPage.PlayerChipsTextBox.Text = _dbService.GetChipsByUserId(fromUserID).ToString();
                            requestWindow.chipsInRequestPage.Text = dbService.GetChipsByUserId(fromUserID).ToString();
                            requestWindow.lobbyPage.PlayerChipsTextBox.Text = dbService.GetChipsByUserId(fromUserID).ToString();
                        }
                    }
                }
            }
            dbService.DeleteRequestsByUserId(dbService.GetUserIdByUserName(currentUserName));
            LoadRequests();
        }
        // Decline all
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dbService.DeleteRequestsByUserId(dbService.GetUserIdByUserName(currentUserName));
            LoadRequests();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dbService.GetChipsByUserId(dbService.GetUserIdByUserName(currentUserName)) == 0)
            {
                try
                {
                    string playerToSend = playerToSendRequest.Text;
                    if (dbService.GetUserIdByUserName(playerToSend) == -1)
                    {
                        MessageBox.Show("Can't find the specified player.");
                        return;
                    }
                    int firstPlayerID = dbService.GetUserIdByUserName(currentUserName);
                    int secondPlayerID = dbService.GetUserIdByUserName(playerToSend);
                    dbService.CreateRequest(firstPlayerID, secondPlayerID);
                }
                catch
                {
                    MessageBox.Show("Can't send multiple request on the same day");
                }
            }
            else
            {
                MessageBox.Show("You must have 0 chips to be able to send requests!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
