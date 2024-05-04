using HarvestHaven.Entities;
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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class TradingInventory : Window
    {
        private TradingUnlocked unlockedScreen;

        public enum InventoryType { Give, Get };
        private InventoryType inventoryType;

        public TradingInventory(TradingUnlocked unlockedScreen, InventoryType inventoryType)
        {
            this.unlockedScreen = unlockedScreen;
            this.inventoryType = inventoryType;
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

        private void CheckForLabel(KeyValuePair<InventoryResource, Resource> pair)
        /* 
         This function changes the label of an entity depending on what is the value in the database.
         */
        {
            if (pair.Value.ResourceType == ResourceType.Carrot)
                carrotLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.Corn)
                cornLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.Wheat)
                wheatLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.Tomato)
                tomatoLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.ChickenMeat)
                chickenLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.Mutton)
                sheepLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.ChickenEgg)
                chickenEggLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.SheepWool)
                woolLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.CowMilk)
                milkLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.DuckEgg)
                duckEggLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.Steak)
                cowLabel.Content = pair.Key.Quantity.ToString();
            else if (pair.Value.ResourceType == ResourceType.DuckMeat)
                duckLabel.Content = pair.Key.Quantity.ToString();
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
                Dictionary<InventoryResource, Resource> resources = await UserService.GetInventoryResources();

                foreach (KeyValuePair<InventoryResource, Resource> pair in resources)
                {
                    CheckForLabel(pair);
                }

                foreach (Label label in labelsGrid.Children)
                {
                    // If we have a label with content higher than 100, we change the font so that it will fit.
                    if (label.Content.ToString().Length > 2) label.FontSize = 27;
                    else label.FontSize = 36;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void carrotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Carrot);
        }

        private void wheatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Wheat);
        }

        private void cornButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Corn);
        }

        private void tomatoButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Tomato);
        }

        private void chickenButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.ChickenMeat);
        }

        private void sheepButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Mutton);
        }

        private void chickenEggButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.ChickenEgg);
        }

        private void woolButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.SheepWool);
        }

        private void milkButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.CowMilk);
        }

        private void duckEggButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.DuckEgg);
        }

        private void cowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.Steak);
        }

        private void duckButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignResourceIcon(ResourceType.DuckMeat);
        }
    }
}
