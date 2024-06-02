using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;

namespace GameWorld.Views
{
    public partial class TradingInventory : Window
    {
        private TradingUnlocked unlockedScreen;
        private readonly IInventoryService inventoryService;

        public enum InventoryType
        {
            Give,
            Get
        }
        private InventoryType inventoryType;

        public TradingInventory(TradingUnlocked unlockedScreen, InventoryType inventoryType, IInventoryService inventoryService)
        {
            this.unlockedScreen = unlockedScreen;
            this.inventoryType = inventoryType;
            this.inventoryService = inventoryService;
            InitializeComponent();
            LoadInventory();
        }

        private void BackToTrading()
        {
            unlockedScreen.Top = this.Top;
            unlockedScreen.Left = this.Left;

            unlockedScreen.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackToTrading();
        }

        public void AssignResourceIcon(ResourceType resourceType)
        {
            unlockedScreen.ChangeIcon(inventoryType, resourceType);

            BackToTrading();
        }

        private async void LoadInventory()
        {
            try
            {
                foreach (Label label in labelsGrid.Children)
                {
                    label.Content = await inventoryService.GetCorrespondingValueForLabel(label.Name);

                    // If we have a label with content higher than 100, we change the font so that it will fit.
                    if (label.Content.ToString().Length > 2)
                    {
                        label.FontSize = 27;
                    }
                    else
                    {
                        label.FontSize = 36;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarrotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Carrot);
        }

        private void WheatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Wheat);
        }

        private void CornButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Corn);
        }

        private void TomatoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Tomato);
        }

        private void ChickenButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.ChickenMeat);
        }

        private void SheepButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Mutton);
        }

        private void ChickenEggButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.ChickenEgg);
        }

        private void WoolButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.SheepWool);
        }

        private void MilkButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.CowMilk);
        }

        private void DuckEggButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.DuckEgg);
        }

        private void CowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Steak);
        }

        private void DuckButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.DuckMeat);
        }
    }
}
