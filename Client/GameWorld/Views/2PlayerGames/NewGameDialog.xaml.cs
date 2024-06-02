using System.Windows;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for NewGameDialog.xaml
    /// </summary>
    public partial class NewGameDialog : Window
    {
        public bool? DialogResult { get; private set; }
        public NewGameDialog()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
