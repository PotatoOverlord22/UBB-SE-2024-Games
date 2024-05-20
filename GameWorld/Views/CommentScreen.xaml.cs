using System.Windows;
using GameWorld.Services;

namespace GameWorld.Views
{
    public partial class CommentScreen : Window
    {
        private VisitedFarm visitedFarm;
        private Guid userId;
        private readonly IUserService userService;

        public CommentScreen(VisitedFarm visitedFarm, Guid userId, IUserService userService)
        {
            this.visitedFarm = visitedFarm;
            this.userId = userId;
            this.userService = userService;

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
                await userService.AddCommentForAnotherUser(userId, CommentTextBox.Text);
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
