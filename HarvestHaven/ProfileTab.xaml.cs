using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for ProfileTab.xaml
    /// </summary>
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

        private void achievementButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToAchievements();
        }

        private void leaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToLeaderboard();
        }

        private void commentsButton_Click(object sender, RoutedEventArgs e)
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

        private void leaderboardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        //public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        //{
        //    // Check if this object is the specified type
        //    if (obj is T)
        //        return obj as T;

        //    // Check for children
        //    int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
        //    if (childrenCount < 1)
        //        return null;

        //    // First check all the children
        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        DependencyObject child = VisualTreeHelper.GetChild(obj, i);
        //        if (child is T)
        //            return child as T;
        //    }

        //    // Then check the childrens children
        //    for (int i = 0; i < childrenCount; i++)
        //    {
        //        DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
        //        if (child != null && child is T)
        //            return child as T;
        //    }

        //    return null;
        //}
    }
}
