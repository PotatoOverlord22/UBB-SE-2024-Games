using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    public partial class LoginPage : Page
    {
        private Frame mainFrame;
       /* private MainWindow mainWindow;*/

       /* public LoginPage(Frame mainFrame, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.mainWindow = mainWindow;
        }*/
        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text when the TextBox gets focus
            TextBox textBox = sender as TextBox;
            textBox.Text = string.Empty;
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // Restore the placeholder text if the TextBox loses focus and is empty
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Input your name";
            }
        }

        private void OnClickLoginButton(object sender, RoutedEventArgs e)
        {
           /* mainWindow.OpenNewWindow(inputNameLoginFirstPage.Text);*/
            inputNameLoginFirstPage.Text = string.Empty;
        }
    }
}
