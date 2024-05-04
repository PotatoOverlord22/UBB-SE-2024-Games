using System.Windows;
using HarvestHaven.Services;

namespace HarvestHaven
{
    public partial class TradingLocked : Window
    {
        private Farm farm;

        public TradingLocked(Farm farm)
        {
            this.farm = farm;

            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            farm.Top = this.Top;
            farm.Left = this.Left;

            farm.Show();
            this.Close();
        }

        private async void Unlock_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await UserService.UnlockTradeHall();

                TradingUnlocked tradingScreen = new TradingUnlocked(farm);

                tradingScreen.Top = this.Top;
                tradingScreen.Left = this.Left;

                tradingScreen.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
