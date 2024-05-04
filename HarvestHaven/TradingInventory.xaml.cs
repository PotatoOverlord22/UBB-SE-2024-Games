using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HarvestHaven.Entities;
using HarvestHaven.Services;

namespace HarvestHaven
{
    public partial class TradingInventory : Window
    {
        private TradingUnlocked unlockedScreen;

        public enum InventoryType
        {
            Give,
            Get
        }
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
            {
                carrotLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.Corn)
            {
                cornLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.Wheat)
            {
                wheatLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.Tomato)
            {
                tomatoLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.ChickenMeat)
            {
                chickenLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.Mutton)
            {
                sheepLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.ChickenEgg)
            {
                chickenEggLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.SheepWool)
            {
                woolLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.CowMilk)
            {
                milkLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.DuckEgg)
            {
                duckEggLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.Steak)
            {
                cowLabel.Content = pair.Key.Quantity.ToString();
            }
            else if (pair.Value.ResourceType == ResourceType.DuckMeat)
            {
                duckLabel.Content = pair.Key.Quantity.ToString();
            }
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
