using System.Windows;
using System.Windows.Controls;
using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;

namespace HarvestHaven
{
    public partial class ProfileTab : Window
    {
        private Farm farmScreen;

        public ProfileTab(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
            SwitchToAchievements();
        }

        private async void SwitchToAchievements()
        {
            achievementList.Visibility = Visibility.Visible;
            leaderboardList.Visibility = Visibility.Hidden;
            commentList.Visibility = Visibility.Hidden;
            try
            {
                List<Achievement> list = await AchievementService.GetAllAchievementsAsync();
                DataContext = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void SwitchToLeaderboard()
        {
            achievementList.Visibility = Visibility.Hidden;
            leaderboardList.Visibility = Visibility.Visible;
            commentList.Visibility = Visibility.Hidden;
            try
            {
                List<User> list = await UserService.GetAllUsersSortedByCoinsAsync();
                DataContext = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void SwitchToComments()
        {
            achievementList.Visibility = Visibility.Hidden;
            leaderboardList.Visibility = Visibility.Hidden;
            commentList.Visibility = Visibility.Visible;
            try
            {
                List<Comment> list = await UserService.GetMyComments();
                DataContext = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void SwitchToVisitedFarm()
        {
        }

        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToAchievements();
        }

        private void LeaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToLeaderboard();
        }

        private void CommentsButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToComments();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }

        private void LeaderboardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            User clickedUser = (User)(sender as Button).DataContext;

            Guid userId = clickedUser.Id;
            if (userId == GameStateManager.GetCurrentUser()?.Id)
            {
                return;
            }
            VisitedFarm visitedFarm = new VisitedFarm(userId, this);
            visitedFarm.Show();
            this.Hide();
        }
    }
}
