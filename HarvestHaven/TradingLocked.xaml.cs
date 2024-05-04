using HarvestHaven.Services;
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
    /// Interaction logic for TradingLocked.xaml
    /// </summary>
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
