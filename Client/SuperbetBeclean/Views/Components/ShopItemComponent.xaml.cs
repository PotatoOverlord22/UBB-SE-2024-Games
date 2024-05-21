using System.Windows;
using System.Windows.Controls;
using SuperbetBeclean.Services;

namespace SuperbetBeclean.Components
{
    public partial class ShopItemComponent : UserControl
    {
        // TODO: Add cost
        // Define dependency properties for data binding
        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register(
            "ImagePath", typeof(string), typeof(ShopItemComponent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
            "ItemName", typeof(string), typeof(ShopItemComponent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ShopUserIdProperty = DependencyProperty.Register(
                       "ShopUserId", typeof(int), typeof(ShopItemComponent), new PropertyMetadata(default(int)));
        // Properties for data binding
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public int ShopUserId
        {
            get { return (int)GetValue(ShopUserIdProperty); }
            set { SetValue(ShopUserIdProperty, value); }
        }

        public ShopItemComponent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var itemName = ItemName; // Access the ItemName property directly
            IDataBaseService dbService = new DataBaseService();
            var itemId = dbService.GetIconIDByIconName(itemName);
            dbService.CreateUserIcon(ShopUserId, itemId);
        }
    }
}
