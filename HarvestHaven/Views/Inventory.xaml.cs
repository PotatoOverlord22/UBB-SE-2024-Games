using System.Windows;
using System.Windows.Controls;
using HarvestHaven.Entities;
using HarvestHaven.Services;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        private Farm farmScreen;
        private readonly IInventoryService inventoryService;

        public Inventory(Farm farmScreen, IInventoryService inventoryService)
        {
            this.farmScreen = farmScreen;
            this.inventoryService = inventoryService;
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
        private async void LoadInventory()
        {
            foreach (Label label in labelsGrid.Children)
            {
                label.Content = await inventoryService.GetCorrespondingValueForLabel(label.Name);

                if (label.Content.ToString().Length > 2)
                {
                    label.FontSize = 27;
                }
            }
        }
    }
}
