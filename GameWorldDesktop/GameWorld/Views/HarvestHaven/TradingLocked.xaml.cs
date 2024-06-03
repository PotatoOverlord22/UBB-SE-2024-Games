using System.Windows;
using System.Windows.Controls;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class TradingLocked : Page
    {
        private Farm farm;
        private readonly IUserService userService;

        public TradingLocked(Farm farm, IUserService userService)
        {
            this.farm = farm;
            this.userService = userService;
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(farm);
        }

        private async void Unlock_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await userService.UnlockTradeHall();

                TradingUnlocked tradingScreen = new TradingUnlocked(farm, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<ITradeService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IResourceService>());
                NavigationService.Navigate(tradingScreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
