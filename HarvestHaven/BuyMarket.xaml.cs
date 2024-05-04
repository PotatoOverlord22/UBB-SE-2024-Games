using HarvestHaven.Entities;
using HarvestHaven.Services;
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
    /// Interaction logic for BuyMarket.xaml
    /// </summary>
    public partial class BuyMarket : Window
    {
        private Farm farmScreen;
        private int row;
        private int column;

        public BuyMarket(Farm farmScreen, int row, int column)
        {
            this.farmScreen = farmScreen;
            this.row = row;
            this.column = column;

            InitializeComponent();
            RefreshGUI();
        }

        private void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null) coinLabel.Content = user.Coins;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            BackToFarm();
        }

        private void BackToFarm()
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.RefreshGUI();
            farmScreen.Show();
            this.Close();
        }

        private async void BuyItem(ItemType itemType)
        {
            try
            {
                await MarketService.BuyItem(row, column, itemType);
                BackToFarm();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void carrotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.CarrotSeeds);
        }

        private void cornButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.CornSeeds);
        }

        private void wheatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.WheatSeeds);
        }

        private void tomatoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.TomatoSeeds);
        }

        private void chickenButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Chicken);
        }

        private void sheepButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Sheep);
        }

        private void cowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Cow);
        }

        private void duckButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Duck);
        }
    }
}
