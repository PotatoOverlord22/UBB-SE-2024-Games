using System.Windows;
using System.Windows.Controls;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;

namespace GameWorld.Views
{
    public partial class SellMarket : Page
    {
        private readonly Farm farmScreen;
        private readonly IMarketService marketService;

        public SellMarket(Farm farmScreen, IMarketService marketService)
        {
            this.farmScreen = farmScreen;
            this.marketService = marketService;
            DataContext = marketService;
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(farmScreen);
            farmScreen.RefreshGUI();
        }

        private async void SellItem(ResourceType resourceType)
        {
            try
            {
                await marketService.SellResource(resourceType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
