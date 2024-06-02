using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for LeaveButton.xaml
    /// </summary>
    public partial class LeaveButton : UserControl
    {
        public event EventHandler ButtonClicked;
        public LeaveButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
