using System.Windows;
using System.Windows.Controls;
using GameWorld.Models;
using GameWorld.Views;
using GameWorld.ViewModels;

namespace GameWorld.Views
{
    public partial class ShopPage : Page
    {
        private Frame mainFrame;
        public ShopPage(Frame mainFrame, MenuWindow menuWindow)
        {
            InitializeComponent();
            DataContext = new MainViewModel(menuWindow.UserChips(), menuWindow.UserId());
            this.mainFrame = mainFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.GoBack();
        }

        public static readonly DependencyProperty BalanceProperty = DependencyProperty.Register(
                       "Balance", typeof(int), typeof(ShopPage), new PropertyMetadata(default(int)));

        public int Balance
        {
            get { return (int)GetValue(BalanceProperty); }
            set { SetValue(BalanceProperty, value); }
        }
    }
}
