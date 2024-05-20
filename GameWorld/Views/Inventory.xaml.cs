﻿using System.Windows;
using System.Windows.Controls;
using GameWorld.Services;
using GameWorld.Entities;

namespace HarvestHaven
{
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