using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using GameWorld.Utils;
using GameWorld;
using GameWorld.Entities;
using GameWorld.Services;

namespace HarvestHaven
{
    public partial class VisitedFarm : Window
    {
        private readonly IFarmService farmService;
        private readonly IUserService userService;
        private List<Image> itemIcons = new List<Image>();

        private Guid userId;
        private ProfileTab profileTab;

        private const int ColumnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        private bool onItemIcon;
        private bool onEnhanceButton;

        public VisitedFarm(Guid userId, ProfileTab profileTab, IFarmService farmService, IUserService userService)
        {
            this.userId = userId;
            this.profileTab = profileTab;
            this.farmService = farmService;
            this.userService = userService;

            InitializeComponent();
            RefreshGUI();
        }

        public async void RefreshGUI()
        {
            User? user = await userService.GetUserByIdAsync(userId);
            if (user != null)
            {
                coinLabel.Content = user.Coins;
                ProfileLabel.Content = user.Username;
            }

            #region Deleting Old Item Icons
            foreach (Image img in itemIcons)
            {
                FarmGrid.Children.Remove(img);
            }
            #endregion

            #region Farm Rendering
            try
            {
                Dictionary<FarmCell, Item> farmCells = await farmService.GetAllFarmCellsForUser(userId);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = ((pair.Key.Row - 1) * ColumnCount) + pair.Key.Column;

                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    ItemType type = pair.Value.ItemType;

                    CreateItemIcon(associatedButton, farmService.GetPicturePathByItemType(type));
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
            thickness.Top += 20;
            EnhanceButton.Margin = thickness;

            EnhanceButton.Visibility = Visibility.Visible;

            ExtractCorrespondingNumberFromFarmGridCellClicked(image.Name);
        }

        private void ItemIcon_Leave(object sender, MouseEventArgs e)
        {
            onItemIcon = false;

            HideEnhanceButton();
        }

        private async void Enhance(object sender, RoutedEventArgs e)
        {
            try
            {
                await farmService.EnchanceCellForUser(userId, clickedRow, clickedColumn);
                HideEnhanceButton(true);
                RefreshGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void HideEnhanceButton(bool forced = false)
        {
            await Task.Delay(10);

            if (onItemIcon || (onEnhanceButton && !forced))
            {
                return;
            }

            EnhanceButton.Visibility = Visibility.Hidden;
        }

        private void ExtractCorrespondingNumberFromFarmGridCellClicked(string cellClickedButtonName)
        {
            string extractedNumber = cellClickedButtonName.Substring(cellClickedButtonName.Length - 2, 2);
            if (int.TryParse(extractedNumber, out int extractedNumberInt))
            {
                ConvertGivenNumberToCorrespondingGridCellPosition(extractedNumberInt);
                return;
            }

            extractedNumber = cellClickedButtonName.Substring(cellClickedButtonName.Length - 1, 1);
            ConvertGivenNumberToCorrespondingGridCellPosition(int.Parse(extractedNumber));
        }

        private void ConvertGivenNumberToCorrespondingGridCellPosition(int extractedNumberFromName)
        {
            int fullRows = extractedNumberFromName / ColumnCount;

            int newNumber = extractedNumberFromName - (fullRows * ColumnCount);
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

        private void EnhanceButton_MouseEnter(object sender, MouseEventArgs e)
        {
            onEnhanceButton = true;
        }

        private void EnhanceButton_MouseLeave(object sender, MouseEventArgs e)
        {
            onEnhanceButton = false;

            HideEnhanceButton();
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            profileTab.Top = this.Top;
            profileTab.Left = this.Left;

            profileTab.Show();
            this.Close();
        }

        private void CommentButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CommentScreen commentScreen = new CommentScreen(this, userId, DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IUserService>());

            commentScreen.Top = this.Top;
            commentScreen.Left = this.Left;

            commentScreen.Show();
            this.Hide();
        }
    }
}
