using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace GameWorld.Views
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
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
                    case "Darts":
                        NavigationService.Navigate(Router.DartsPage);
                        break;
                    case "Connect4":
                        NavigationService.Navigate(Router.ConnectPage);
                        break;
                    case "Chess":
                        object[] list = { Router.ChessMode };
                        NavigationService.Navigate(Router.ChessPage);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                List<Object> list = new();
                list.Add(Router.AiDifficulty);
                switch (Router.GameType)
                {
                    case "Chess":
                        list.Add(Router.ChessMode);
                        NavigationService.Navigate(Router.ChessPage);
                        break;
                    case "Obstruction":
                        list.Add(Router.ObstructionMode);
                        list.Add(Router.ObstructionMode);
                        NavigationService.Navigate(Router.ObstructionPage);
                        break;
                    case "Darts":
                        NavigationService.Navigate(Router.DartsPage);
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
