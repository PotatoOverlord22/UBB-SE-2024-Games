using HarvestHaven.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CommentScreen.xaml
    /// </summary>
    public partial class CommentScreen : Window
    {
        VisitedFarm visitedFarm;
        Guid userId;

        public CommentScreen(VisitedFarm visitedFarm, Guid userId)
        {
            this.visitedFarm = visitedFarm;
            this.userId = userId;

            InitializeComponent();
        }

        private void BackToVisitedFarm()
        {
            visitedFarm.Top = this.Top;
            visitedFarm.Left = this.Left;

            visitedFarm.Show();
            this.Close();
        }

        private async void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            try
            {
                await UserService.AddCommentForAnotherUser(userId, CommentTextBox.Text);
                BackToVisitedFarm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            BackToVisitedFarm();
        }
    }
}
