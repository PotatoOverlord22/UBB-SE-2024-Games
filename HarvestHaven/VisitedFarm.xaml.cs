using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for VisitedFarm.xaml
    /// </summary>
    public partial class VisitedFarm : Window
    {
        private List<Image> itemIcons = new List<Image>();

        #region Image Paths
        private const string carrotPath = "Assets/Sprites/Items/carrot.png";
        private const string cornPath = "Assets/Sprites/Items/corn.png";
        private const string wheatPath = "Assets/Sprites/Items/wheat.png";
        private const string tomatoPath = "Assets/Sprites/Items/tomato.png";
        private const string chickenPath = "Assets/Sprites/Items/chicken.png";
        private const string sheepPath = "Assets/Sprites/Items/sheep.png";
        private const string cowPath = "Assets/Sprites/Items/cow.png";
        private const string duckPath = "Assets/Sprites/Items/duck.png";
        #endregion

        private Guid userId;
        private ProfileTab profileTab;

        private const int columnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        private bool onItemIcon;
        private bool onEnhanceButton;

        public VisitedFarm(Guid userId, ProfileTab profileTab)
        {
            this.userId = userId;
            this.profileTab = profileTab;

            InitializeComponent();
            RefreshGUI();
        }

        public async void RefreshGUI()
        {
            User? user = await UserService.GetUserByIdAsync(userId);
            if (user != null)
            {
                coinLabel.Content = user.Coins;
                ProfileLabel.Content = user.Username;
            }

            #region Deleting Old Item Icons
            foreach (Image img in itemIcons)
                FarmGrid.Children.Remove(img);
            #endregion

            #region Farm Rendering
            try
            {
                Dictionary<FarmCell, Item> farmCells = await FarmService.GetAllFarmCellsForUser(userId);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = (pair.Key.Row - 1) * columnCount + pair.Key.Column;

                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    ItemType type = pair.Value.ItemType;
                    string path = "";
                    if (type == ItemType.CarrotSeeds) path = carrotPath;
                    else if (type == ItemType.CornSeeds) path = cornPath;
                    else if (type == ItemType.WheatSeeds) path = wheatPath;
                    else if (type == ItemType.TomatoSeeds) path = tomatoPath;
                    else if (type == ItemType.Chicken) path = chickenPath;
                    else if (type == ItemType.Duck) path = duckPath;
                    else if (type == ItemType.Sheep) path = sheepPath;
                    else path = cowPath;

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
                if (property.CanWrite) property.SetValue(newImage, property.GetValue(itemIcon));
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

            SetRowColumn(image.Name);
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
                await FarmService.EnchanceCellForUser(userId, clickedRow, clickedColumn);
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

            if (onItemIcon || onEnhanceButton && !forced) return;

            EnhanceButton.Visibility = Visibility.Hidden;
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
            int fullRows = number / columnCount;

            int newNumber = number - (fullRows * columnCount);
            if (newNumber == 0)
            {
                this.clickedRow = fullRows;
                this.clickedColumn = columnCount;
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
            CommentScreen commentScreen = new CommentScreen(this, userId);

            commentScreen.Top = this.Top;
            commentScreen.Left = this.Left;

            commentScreen.Show();
            this.Hide();
        }
    }
}
