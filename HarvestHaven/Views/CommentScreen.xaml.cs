using System.Windows;
using HarvestHaven.Services;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for CommentScreen.xaml
    /// </summary>
    public partial class CommentScreen : Window
    {
        private VisitedFarm visitedFarm;
        private Guid userId;

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
