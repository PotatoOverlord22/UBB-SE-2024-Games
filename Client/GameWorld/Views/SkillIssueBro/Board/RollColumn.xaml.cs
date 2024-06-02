using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for Column1.xaml
    /// </summary>
    public partial class RollColumn : UserControl
    {
        public RollColumn()
        {
            InitializeComponent();
            rollButton.ButtonClicked += RollButton_ButtonClicked;
            leaveButton.ButtonClicked += LeaveButton_ButtonClicked;
        }

        private void RollButton_ButtonClicked(object sender, EventArgs e)
        {
            rollButton.Visibility = Visibility.Collapsed;
        }

        private void LeaveButton_ButtonClicked(object sender, EventArgs e)
        {
            leaveButton.Visibility = Visibility.Collapsed; // TODO leave
        }
    }
}
