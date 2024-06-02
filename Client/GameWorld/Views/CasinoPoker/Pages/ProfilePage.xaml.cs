using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GameWorldClassLibrary.Models;
using GameWorld.ViewModels;
namespace GameWorld.Views
{
    public partial class ProfilePage : Page
    {
        private Frame mainFrame;
        private MenuWindow mainWindow;
        public ProfilePage(Frame mainFrame, MenuWindow mainWindow)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.mainWindow = mainWindow;
            User player = mainWindow.Player();
            DataContext = new ProfileViewModel(mainWindow);
            if (!string.IsNullOrEmpty(player.UserCurrentIconPath))
            {
                profilePageUserAvatar.ImageSource = new BitmapImage(new Uri(player.UserCurrentIconPath, UriKind.Absolute));
            }

            profilePageUsernameTextBlock.Text = mainWindow.UserName();
            profilePageChipsTextBlock.Text = mainWindow.UserChips().ToString();
            profilePageDailyStreakTextBlock.Text = mainWindow.UserStreak().ToString();
            profilePageLevelTextBlock.Text = mainWindow.UserLevel().ToString() + ": ";
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.NavigationService.GoBack();
        }
    }
}
