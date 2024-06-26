﻿using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Services;

namespace GameWorld.Views
{
    public partial class Farm : Page
    {
        private List<Image> itemIcons = new List<Image>();
        private readonly IFarmService farmService;
        private readonly IUserService userService;

        private const int ColumnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        private bool onFarmCell;
        private bool onBuyButton;

        private bool onItemIcon;
        private bool onInteractionButton;

        public Farm(IFarmService farmService, IUserService userService)
        {
            this.farmService = farmService;
            this.userService = userService;
            InitializeComponent();
            RefreshGUI();
        }
        private void InventoryButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Inventory(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IInventoryService>()));
        }

        private void ShopButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new SellMarket(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IMarketService>()));
        }

        private void QuitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }

        private void ProfileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProfileTab profileTab = new ProfileTab(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IAchievementService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());
            NavigationService.Navigate(profileTab);
        }

        private void TradingHallButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool unlocked = userService.IsTradeHallUnlocked();

            if (unlocked)
            {
                TradingUnlocked tradingScreen = new TradingUnlocked(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<ITradeService>(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IResourceService>());
                NavigationService.Navigate(tradingScreen);
            }
            else
            {
                TradingLocked tradingScreen = new TradingLocked(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());
                NavigationService.Navigate(tradingScreen);
            }
        }

        private void OpenBuyMarket(object sender, MouseButtonEventArgs e)
        {
            HideBuyButton(true);

            BuyMarket market = new BuyMarket(this, clickedRow, clickedColumn, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IMarketService>());
            NavigationService.Navigate(market);
        }

        public async void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                coinLabel.Content = user.Coins;
                ProfileLabel.Content = user.Username;
            }

            try
            {
                InventoryResource water = await userService.GetInventoryResourceByType(ResourceType.Water, GameStateManager.GetCurrentUserId());

                if (water == null)
                {
                    waterLabel.Content = "0";
                }
                else
                {
                    waterLabel.Content = water.Quantity;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            foreach (Image img in itemIcons)
            {
                FarmGrid.Children.Remove(img);
            }

            #region Farm Rendering
            try
            {
                Dictionary<FarmCell, Item> farmCells = await farmService.GetAllFarmCellsForUser(user.Id);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = ((pair.Key.Row - 1) * ColumnCount) + pair.Key.Column;

                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    ItemType type = pair.Value.ItemType;
                    string path = farmService.GetPicturePathByItemType(type);

                    CreateItemIcon(associatedButton, path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            #endregion
        }

        private void CreateItemIcon(Button associatedButton, string imagePath)
        {
            Image newImage = new Image();

            PropertyInfo[] properties = typeof(Image).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(newImage, property.GetValue(itemIcon));
                }
            }
            newImage.Visibility = Visibility.Visible;

            newImage.Margin = associatedButton.Margin;

            newImage.Source = new BitmapImage(new Uri("pack://application:,,," + imagePath));

            newImage.MouseDown += ItemIcon_Click;
            newImage.MouseLeave += ItemIcon_Leave;

            newImage.Name = "Image" + associatedButton.Name;

            FarmGrid.Children.Add(newImage);
            itemIcons.Add(newImage);
        }

        private void ItemIcon_Click(object sender, MouseButtonEventArgs e)
        {
            onItemIcon = true;

            Image image = (Image)sender;
            Thickness thickness = image.Margin;
            thickness.Left += 60;
            thickness.Top += 13;
            InteractionButtons.Margin = thickness;

            InteractionButtons.Visibility = Visibility.Visible;

            SetRowColumn(image.Name);
        }

        private void ItemIcon_Leave(object sender, MouseEventArgs e)
        {
            onItemIcon = false;

            HideInteractionButtons();
        }

        private void InteractionButtons_MouseEnter(object sender, MouseEventArgs e)
        {
            onInteractionButton = true;
        }

        private void InteractionButtons_MouseLeave(object sender, MouseEventArgs e)
        {
            onInteractionButton = false;

            HideInteractionButtons();
        }

        private async void HideInteractionButtons(bool forced = false)
        {
            await Task.Delay(10);

            if (onItemIcon || (onInteractionButton && !forced))
            {
                return;
            }

            InteractionButtons.Visibility = Visibility.Hidden;
        }

        private async void Interact(object sender, RoutedEventArgs e)
        {
            try
            {
                await farmService.InteractWithCell(this.clickedRow, this.clickedColumn);
                HideInteractionButtons(true);
                RefreshGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Destroy(object sender, RoutedEventArgs e)
        {
            try
            {
                await farmService.DestroyCell(this.clickedRow, this.clickedColumn);
                HideInteractionButtons(true);
                RefreshGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowBuyButton(Button button)
        {
            Thickness thickness = button.Margin;
            thickness.Left += 1;
            thickness.Top += 15;
            BuyButton.Margin = thickness;

            BuyButton.Visibility = Visibility.Visible;
        }

        private async void HideBuyButton(bool forced = false)
        {
            await Task.Delay(10);

            if (onFarmCell || (onBuyButton && !forced))
            {
                return;
            }

            BuyButton.Visibility = Visibility.Hidden;
        }

        private void SetRowColumn(string name)
        {
            string possibleNumber = name.Substring(name.Length - 2, 2);
            if (int.TryParse(possibleNumber, out int number))
            {
                ConvertToRowColumn(number);
                return;
            }

            possibleNumber = name.Substring(name.Length - 1, 1);
            number = int.Parse(possibleNumber);
            ConvertToRowColumn(number);
        }

        private void ConvertToRowColumn(int number)
        {
            int fullRows = number / ColumnCount;

            int newNumber = number - (fullRows * ColumnCount);
            if (newNumber == 0)
            {
                this.clickedRow = fullRows;
                this.clickedColumn = ColumnCount;
            }
            else
            {
                this.clickedRow = fullRows + 1;
                this.clickedColumn = newNumber;
            }
        }

        private void Farm_Click(object sender, RoutedEventArgs e)
        {
            onFarmCell = true;
            Button button = (Button)sender;

            ShowBuyButton(button);
            SetRowColumn(button.Name);
        }

        private void Farm_MouseLeave(object sender, MouseEventArgs e)
        {
            onFarmCell = false;

            HideBuyButton();
        }

        private void BuyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            onBuyButton = true;
        }

        private void BuyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            onBuyButton = false;

            HideBuyButton();
        }
    }
}
