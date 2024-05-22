using System.Windows;
using System.Windows.Input;

namespace GameWorld.Views
{
    public partial class ChallengesWindow : Window
    {
        public ChallengesWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Assuming the height of the upper part is 60 (adjust as needed)
                if (e.GetPosition(this).Y < 60)
                {
                    // Drag the window
                    DragMove();
                }
            }
            catch
            {
            }
    }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the window when the button is clicked
        }
    }
}
