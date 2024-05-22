using System.Windows.Controls;

namespace GameWorld.Views
{
    using System.IO;
    using System.Windows;
    public partial class RulesWindow : Window
    {
        private const string HtmlFilePath = @".\assets\index.html";

        public RulesWindow()
        {
            InitializeComponent();
            LoadHtmlContent();
        }

        private void CloseButtonRulesWindow_Click(object sender, RoutedEventArgs routedEvent)
        {
            this.Close();
        }
        private void LoadHtmlContent()
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Combine the solution directory with the relative HTML file path
            string htmlFilePath = Path.Combine(solutionDirectory, HtmlFilePath);

            if (File.Exists(htmlFilePath))
            {
                // Read HTML content from the file
                string htmlContent = File.ReadAllText(htmlFilePath);

                // Display the HTML content in the WebBrowser control
                WebBrowser.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("Rules file not found.");
            }
        }
    }
}
