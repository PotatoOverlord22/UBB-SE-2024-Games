using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameWorld.Models;
using GameWorld.Services;
namespace GameWorld.Views
{
    public partial class RequestsWindow : Window
    {
        private List<string> requests;
        private IUserService userService;
        private LobbyPage lobbyPage;
        private User currentUser;
        public RequestsWindow(User currentUser, LobbyPage lobbyPage, string userName, IUserService userService)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.userService = userService; // Initialize the database service
            this.lobbyPage = lobbyPage;
            // Call a method to load and display requests
            LoadRequests();
            chipsInRequestPage.Text = userService.GetChipsByUserId(currentUser.Id).ToString();
        }

        private void LoadRequests()
        {
            requests = userService.GetAllRequestsByToUserID(currentUser.Id); // Get requests from the database
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
            List<Tuple<Guid, Guid>> requests = userService.GetAllRequestsByToUserIDSimplified(currentUser.Id);

            foreach (Tuple<Guid, Guid> request in requests)
            {
                Guid fromUserID = request.Item1;
                Guid toUserID = request.Item2;
                int numberChips = userService.GetChipsByUserId(fromUserID) + 3000;
                userService.UpdateUserChips(fromUserID, userService.GetChipsByUserId(fromUserID) + 3000);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(RequestsWindow))
                    {
                        RequestsWindow requestWindow = (RequestsWindow)window;
                        if (requestWindow.currentUser.Id == fromUserID)
                        {
                            // _lobbyPage.PlayerChipsTextBox.Text = _dbService.GetChipsByUserId(fromUserID).ToString();
                            requestWindow.chipsInRequestPage.Text = userService.GetChipsByUserId(fromUserID).ToString();
                            requestWindow.lobbyPage.PlayerChipsTextBox.Text = userService.GetChipsByUserId(fromUserID).ToString();
                        }
                    }
                }
            }
            // dbService.DeleteRequestsByUserId(currentUser.Id);
            LoadRequests();
        }
        // Decline all
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // dbService.DeleteRequestsByUserId(currentUser.Id);
            LoadRequests();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (userService.GetChipsByUserId(currentUser.Id) == 0)
            {
                try
                {
                    string playerToSend = playerToSendRequest.Text;
                    MessageBox.Show($"Sent request from player {currentUser.Username} to {playerToSend}");
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
