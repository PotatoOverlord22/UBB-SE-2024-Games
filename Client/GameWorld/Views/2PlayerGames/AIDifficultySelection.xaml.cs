using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameWorld.Utils;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for AIDifficultySelection.xaml
    /// </summary>
    public partial class AIDifficultySelection : Page
    {
        private bool isDragging = false;
        private Point lastMousePosition;
        public AIDifficultySelection()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Router.OpponentPage);
        }

        private void EasyMode_Click(object sender, RoutedEventArgs e)
        {
            Router.AiDifficulty = "easy";
            // NavigationService.Navigate(Router.LoadingPage);
        }

        private void MediumMode_Click(object sender, RoutedEventArgs e)
        {
            Router.AiDifficulty = "medium";
            // NavigationService.Navigate(Router.LoadingPage);
        }

        private void HardMode_Click(object sender, RoutedEventArgs e)
        {
            Router.AiDifficulty = "hard";
            //NavigationService.Navigate(Router.LoadingPage);
        }
        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - lastMousePosition.X;
                double deltaY = currentPosition.Y - lastMousePosition.Y;

                // Update the position of the page
                Canvas.SetLeft(this, Canvas.GetLeft(this) + deltaX);
                Canvas.SetTop(this, Canvas.GetTop(this) + deltaY);

                lastMousePosition = currentPosition;
            }
        }

        private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }
    }
}
