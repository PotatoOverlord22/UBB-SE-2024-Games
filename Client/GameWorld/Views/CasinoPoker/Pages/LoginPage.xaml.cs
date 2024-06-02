using System.Windows;
using System.Windows.Controls;
using GameWorldClassLibrary.Services.Interfaces;

namespace GameWorld.Views
{
    public partial class LoginPage : Window
    {
        private ICasinoPokerMainService service;

        public LoginPage(ICasinoPokerMainService service)
        {
            InitializeComponent();
            this.service = service;
        }
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
            OpenNewWindow(inputNameLoginFirstPage.Text);
            inputNameLoginFirstPage.Text = string.Empty;
        }

        public void OpenNewWindow(string username)
        {
            service.AddWindow(username);
        }
    }
}
