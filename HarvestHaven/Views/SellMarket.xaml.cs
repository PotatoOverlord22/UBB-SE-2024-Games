using System.Windows;
using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;

namespace HarvestHaven
{
    public partial class SellMarket : Window
    {
        private Farm farmScreen;

        public SellMarket(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
            RefreshGui();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.RefreshGUI();
            farmScreen.Show();
            this.Close();
        }

        private async void SellItem(ResourceType resourceType)
        {
            try
            {
                await MarketService.SellResource(resourceType);
                RefreshGui();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGui()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                PriceLabel.Content = user.Coins;
            }
        }

        private void SellCarrotButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Carrot);
        }

        private void SellCornButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Corn);
        }

        private void SellWheatButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Wheat);
        }

        private void SellTomatoButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Tomato);
        }

        private void SellChickenMeatButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.ChickenMeat);
        }

        private void SellMuttonButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Mutton);
        }

        private void SellChickenEggButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.ChickenEgg);
        }

        private void SellWoolButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.SheepWool);
        }

        private void SellMilkButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.CowMilk);
        }

        private void SelllDuckEggButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.DuckEgg);
        }

        private void SellSteakButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.Steak);
        }

        private void SellDuckMeatButton_Click(object sender, RoutedEventArgs e)
        {
            SellItem(ResourceType.DuckMeat);
        }
    }
}
