using System.Windows;
using System.Windows.Media.Imaging;
using GameWorld.Models;
using GameWorld.Resources.Utils;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;
using static GameWorld.Views.TradingInventory;

namespace GameWorld.Views
{
    public partial class TradingUnlocked : Window
    {
        private readonly ITradeService tradeService;
        private readonly IResourceService resourceService;
        private Farm farmScreen;

        private List<Trade> tradeList;
        private ResourceType getResource;
        private ResourceType giveResource;

        public TradingUnlocked(Farm farmScreen, ITradeService tradeService, IResourceService resourceService)
        {
            this.farmScreen = farmScreen;
            this.tradeService = tradeService;
            this.resourceService = resourceService;
            InitializeComponent();

            GetAllTrades();
        }

        public void ChangeIcon(InventoryType inventoryType, ResourceType resourceType)
        {
            if (inventoryType == InventoryType.Get)
            {
                Get_Button.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType), UriKind.Relative));
                getResource = resourceType;
            }
            else if (inventoryType == InventoryType.Give)
            {
                Give_Button.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType), UriKind.Relative));
                giveResource = resourceType;
            }
        }
        private void SwitchToCreateTrade()
        {
            this.Confirm_Cancel_Button.Content = "Confirm";
            Give_TextBox.IsReadOnly = false;
            Give_TextBox.Text = "0";
            Get_TextBox.IsReadOnly = false;
            Get_TextBox.Text = "0";
            getResource = ResourceType.Water;
            giveResource = ResourceType.Water;
            Give_Button.Source = new BitmapImage(new Uri("/Resources/Assets/Sprites/backpack_icon.png", UriKind.Relative));
            Get_Button.Source = new BitmapImage(new Uri("/Resources/Assets/Sprites/backpack_icon.png", UriKind.Relative));
            this.Get_Button.IsEnabled = true;
            this.Give_Button.IsEnabled = true;
        }

        private async void SwitchToCancelTrade(Trade trade)
        {
            this.Confirm_Cancel_Button.Content = "Cancel";

            Resource resource1 = await resourceService.GetResourceByIdAsync(trade.ResourceToGiveId);
            ResourceType resourceType1 = resource1.ResourceType;
            Give_TextBox.IsReadOnly = true;
            Give_TextBox.Text = trade.ResourceToGiveQuantity.ToString();
            Give_Button.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType1), UriKind.Relative));

            Resource resource2 = await resourceService.GetResourceByIdAsync(trade.ResourceToGetResourceId);
            ResourceType resourceType2 = resource2.ResourceType;
            Get_TextBox.IsReadOnly = true;
            Get_TextBox.Text = trade.ResourceToGetQuantity.ToString();
            Get_Button.Source = new BitmapImage(new Uri("/Resources/Assets/Sprites/backpack_icon.png", UriKind.Relative));
            Get_Button.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType2), UriKind.Relative));

            this.Get_Button.IsEnabled = false;
            this.Give_Button.IsEnabled = false;
        }

        private async void GetAllTrades()
        {
            try
            {
                tradeList = await tradeService.GetAllTradesExceptCreatedByLoggedUser();
                foreach (Trade item in tradeList)
                {
                    TradingPanel tradingPanel = new (item);

                    Resource resource1 = await resourceService.GetResourceByIdAsync(item.ResourceToGetResourceId);
                    ResourceType resourceType1 = resource1.ResourceType;
                    tradingPanel.LabelGive.Content = item.ResourceToGetQuantity;
                    tradingPanel.ImageGive.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType1), UriKind.Relative));

                    Resource resource2 = await resourceService.GetResourceByIdAsync(item.ResourceToGiveId);
                    ResourceType resourceType2 = resource2.ResourceType;
                    tradingPanel.LabelGet.Content = item.ResourceToGiveQuantity;
                    tradingPanel.ImageGet.Source = new BitmapImage(new Uri(tradeService.GetPicturePathByResourceType(resourceType2), UriKind.Relative));

                    Trades_List.Items.Add(tradingPanel);
                    tradingPanel.AcceptButton.Click += (sender, e) => AcceptButton_Click(sender, e, tradingPanel);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            try
            {
                Trade playerTrade = await tradeService.GetUserTradeAsync(GameStateManager.GetCurrentUserId());
                if (playerTrade == null)
                {
                    SwitchToCreateTrade();
                }
                else
                {
                    SwitchToCancelTrade(playerTrade);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e, TradingPanel tradingPanel)
        {
            // Accept trade
            try
            {
                await tradeService.PerformTradeAsync(tradingPanel.Trade.Id);
                Trades_List.Items.Remove(tradingPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }

        private void Give_Button_Click(object sender, RoutedEventArgs e)
        {
            // Open inventory and select the resource you want to give
            // and return the resource type
            TradingInventory inventoryScreen = new TradingInventory(this, InventoryType.Give, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IInventoryService>());

            inventoryScreen.Top = this.Top;
            inventoryScreen.Left = this.Left;

            inventoryScreen.Show();

            this.Hide();
        }

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {
            // Open inventory and select the resource you want to give
            // and return the resource type
            TradingInventory inventoryScreen = new TradingInventory(this, InventoryType.Get, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IInventoryService>());

            inventoryScreen.Top = this.Top;
            inventoryScreen.Left = this.Left;

            inventoryScreen.Show();

            this.Hide();
        }

        private async void Confirm_Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Confirm_Cancel_Button.Content.Equals("Confirm"))
            {
                // Create trade
                string amountGet = Get_TextBox.Text;
                string amountGive = Give_TextBox.Text;
                try
                {
                    await tradeService.CreateTradeAsync(giveResource, amountGet, getResource, amountGive);
                    this.Confirm_Cancel_Button.Content = "Cancel";
                    Give_TextBox.IsReadOnly = true;
                    Get_TextBox.IsReadOnly = true;
                    this.Get_Button.IsEnabled = false;
                    this.Give_Button.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Input should be a positive integer!" || ex.Message == "Select the resources to give and get!")
                    {
                        _ = MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Input should be a positive integer!");
                    }
                }
            }
            else
            {
                // Cancel trade
                try
                {
                    Trade playerTrade = await tradeService.GetUserTradeAsync(GameStateManager.GetCurrentUserId());
                    await tradeService.CancelTradeAsync(playerTrade.Id);

                    SwitchToCreateTrade();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
