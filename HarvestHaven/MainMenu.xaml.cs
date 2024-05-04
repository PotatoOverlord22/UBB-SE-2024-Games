using HarvestHaven.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            RefreshGUI();
        }

        private void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null) GreetingLabel.Content = "Welcome, " + user.Username;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Farm farmScreen = new Farm();

            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
