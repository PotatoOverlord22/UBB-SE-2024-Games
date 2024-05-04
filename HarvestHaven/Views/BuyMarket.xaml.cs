using System.Windows;
using System.Windows.Input;
using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;

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
            if (user != null)
            {
                coinLabel.Content = user.Coins;
            }
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

        private void CarrotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.CarrotSeeds);
        }

        private void CornButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.CornSeeds);
        }

        private void WheatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.WheatSeeds);
        }

        private void TomatoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.TomatoSeeds);
        }

        private void ChickenButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Chicken);
        }

        private void SheepButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Sheep);
        }

        private void CowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Cow);
        }

        private void DuckButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BuyItem(ItemType.Duck);
        }
    }
}
