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
    public partial class Inventory : Window
    {
        private Farm farmScreen;

        public Inventory(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
            LoadInventory();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
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

        private async void LoadInventory()
        {
            try
            {
                Dictionary<InventoryResource, Resource> resources = await UserService.GetInventoryResources();

                foreach(KeyValuePair<InventoryResource, Resource> pair in resources)
                {
                    CheckForLabel(pair);
                }

                foreach(Label label in labelsGrid.Children)
                {
                    // If we have a label with content higher than 100, we change the font so that it will fit.
                    if (label.Content.ToString().Length > 2)
                    {
                        label.FontSize = 27;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
