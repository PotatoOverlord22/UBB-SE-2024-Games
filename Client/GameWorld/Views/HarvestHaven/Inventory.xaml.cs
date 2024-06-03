using System.Windows;
using System.Windows.Controls;
using GameWorldClassLibrary.Services;

namespace GameWorld.Views
{
    public partial class Inventory : Page
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
            NavigationService.Navigate(farmScreen);
        }
        private async void LoadInventory()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
