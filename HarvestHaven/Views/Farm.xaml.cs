using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HarvestHaven.Entities;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HarvestHaven
{
    public partial class Farm : Window
    {
        private List<Image> itemIcons = new List<Image>();
        private readonly IFarmService farmService;

        #region Image Paths
        private const string CarrotPath = "Assets/Sprites/Items/carrot.png";
        private const string CornPath = "Assets/Sprites/Items/corn.png";
        private const string WheatPath = "Assets/Sprites/Items/wheat.png";
        private const string TomatoPath = "Assets/Sprites/Items/tomato.png";
        private const string ChickenPath = "Assets/Sprites/Items/chicken.png";
        private const string SheepPath = "Assets/Sprites/Items/sheep.png";
        private const string CowPath = "Assets/Sprites/Items/cow.png";
        private const string DuckPath = "Assets/Sprites/Items/duck.png";
        #endregion

        private const int ColumnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        private bool onFarmCell;
        private bool onBuyButton;

        private bool onItemIcon;
        private bool onInteractionButton;

        public Farm(IFarmService farmService)
        {
            this.farmService = farmService;
            InitializeComponent();
            RefreshGUI();
        }

        #region Screen Transitions
        private void InventoryButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Inventory inventoryScreen = new Inventory(this);

            inventoryScreen.Top = this.Top;
            inventoryScreen.Left = this.Left;

            inventoryScreen.Show();

            this.Hide();
        }

        private void ShopButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SellMarket sellMarket = new SellMarket(this, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IMarketService>());

            sellMarket.Top = this.Top;
            sellMarket.Left = this.Left;

            sellMarket.Show();

            this.Hide();
        }

        private void QuitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ProfileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProfileTab profileTab = new ProfileTab(this);

            profileTab.Top = this.Top;
            profileTab.Left = this.Left;

            profileTab.Show();

            this.Hide();
        }

        private void TradingHallButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool unlocked = UserService.IsTradeHallUnlocked();

            if (unlocked)
            {
                TradingUnlocked tradingScreen = new TradingUnlocked(this);

                tradingScreen.Top = this.Top;
                tradingScreen.Left = this.Left;

                tradingScreen.Show();

                this.Hide();
            }
            else
            {
                TradingLocked tradingScreen = new TradingLocked(this);

                tradingScreen.Top = this.Top;
                tradingScreen.Left = this.Left;

                tradingScreen.Show();

                this.Hide();
            }
        }

        private void OpenBuyMarket(object sender, MouseButtonEventArgs e)
        {
            HideBuyButton(true);

            BuyMarket market = new BuyMarket(this, clickedRow, clickedColumn, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IMarketService>());

            market.Top = this.Top;
            market.Left = this.Left;

            market.Show();

            this.Hide();
        }
        #endregion

        public async void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                coinLabel.Content = user.Coins;
                ProfileLabel.Content = user.Username;
            }

            #region Update Water
            try
            {
                InventoryResource water = await UserService.GetInventoryResourceByType(ResourceType.Water);

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
            #endregion

            #region Deleting Old Item Icons
            foreach (Image img in itemIcons)
            {
                FarmGrid.Children.Remove(img);
            }
            #endregion

            #region Farm Rendering
            try
            {
                Dictionary<FarmCell, Item> farmCells = await farmService.GetAllFarmCellsForUser(user.Id);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = ((pair.Key.Row - 1) * ColumnCount) + pair.Key.Column;

                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    ItemType type = pair.Value.ItemType;
                    string path = string.Empty;
                    if (type == ItemType.CarrotSeeds)
                    {
                        path = CarrotPath;
                    }
                    else if (type == ItemType.CornSeeds)
                    {
                        path = CornPath;
                    }
                    else if (type == ItemType.WheatSeeds)
                    {
                        path = WheatPath;
                    }
                    else if (type == ItemType.TomatoSeeds)
                    {
                        path = TomatoPath;
                    }
                    else if (type == ItemType.Chicken)
                    {
                        path = ChickenPath;
                    }
                    else if (type == ItemType.Duck)
                    {
                        path = DuckPath;
                    }
                    else if (type == ItemType.Sheep)
                    {
                        path = SheepPath;
                    }
                    else
                    {
                        path = CowPath;
                    }

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

            newImage.Source = new BitmapImage(new Uri("pack://application:,,,/" + imagePath));

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
