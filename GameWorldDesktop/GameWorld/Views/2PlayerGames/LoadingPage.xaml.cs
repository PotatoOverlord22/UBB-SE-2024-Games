using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using GameWorld.Utils;

namespace GameWorld.Views
{
    public partial class LoadingPage : Page
    {
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        private TimeSpan desiredTime = TimeSpan.FromSeconds(10); // Change to desired time

        public LoadingPage()
        {
            InitializeComponent();
            Loaded += LoadingPage_Loaded;
        }

        private void LoadingPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Router.OnlineGame)
            {
                switch (Router.GameType)
                {
                    case "Obstruction":
                        object[] list2 = { Router.ObstructionMode, Router.ObstructionMode };
                        NavigationService.Navigate(Router.ObstructionPage);
                        break;
                    case "Connect4":
                        NavigationService.Navigate(Router.ConnectPage);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                List<object> list = new List<object>();
                list.Add(Router.AiDifficulty);
                switch (Router.GameType)
                {
                    case "Obstruction":
                        list.Add(Router.ObstructionMode);
                        list.Add(Router.ObstructionMode);
                        NavigationService.Navigate(Router.ObstructionPage);
                        break;
                    case "Connect4":
                        NavigationService.Navigate(Router.ConnectPage);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
