using System.Windows;
using System.Windows.Controls;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;
using GameWorld.Resources.Utils;
using GameWorld.Models;

namespace GameWorld.Views
{
    public partial class ProfileTab : Window
    {
        private Farm farmScreen;
        private readonly IAchievementService achievementService;
        private readonly IUserService userService;

        public ProfileTab(Farm farmScreen, IAchievementService achievementService, IUserService userService)
        {
            this.farmScreen = farmScreen;
            this.achievementService = achievementService;
            this.userService = userService;
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
                List<Achievement> list = await achievementService.GetAllAchievementsAsync();
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
                List<User> list = await userService.GetAllUsersSortedByCoinsAsync();
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
                List<Comment> list = await userService.GetMyComments();
                DataContext = list;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
        private void User_Click(object sender, RoutedEventArgs e)
        {
            User clickedUser = (User)(sender as Button).DataContext;

            Guid userId = clickedUser.Id;
            if (userId == GameStateManager.GetCurrentUser()?.Id)
            {
                return;
            }
            VisitedFarm visitedFarm = new VisitedFarm(clickedUser, this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IFarmService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());
            visitedFarm.Show();
            this.Hide();
        }
    }
}
