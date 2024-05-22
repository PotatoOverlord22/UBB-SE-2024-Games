using System.Windows;
using System.Windows.Controls;

namespace GameWorld.Views
{
    public partial class OwnedItemComponent : UserControl
    {
        public static readonly DependencyProperty OwnedImagePathProperty = DependencyProperty.Register(
            "OwnedImagePath", typeof(string), typeof(OwnedItemComponent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty OwnedItemNameProperty = DependencyProperty.Register(
            "OwnedItemName", typeof(string), typeof(OwnedItemComponent), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty OwnedUserIdProperty = DependencyProperty.Register(
            "OwnedUserId", typeof(int), typeof(OwnedItemComponent), new PropertyMetadata(default(int)));

        // Properties for data binding
        public string OwnedImagePath
        {
            get { return (string)GetValue(OwnedImagePathProperty); }
            set { SetValue(OwnedImagePathProperty, value); }
        }

        public string OwnedItemName
        {
            get { return (string)GetValue(OwnedItemNameProperty); }
            set { SetValue(OwnedItemNameProperty, value); }
        }

        public int OwnedUserId
        {
            get { return (int)GetValue(OwnedUserIdProperty); }
            set { SetValue(OwnedUserIdProperty, value); }
        }

        public OwnedItemComponent()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*var itemName = OwnedItemName; // Access the ItemName property directly
            IDataBaseService dbService = new DataBaseService();
            var itemId = dbService.GetIconIDByIconName(itemName);
            Console.WriteLine(OwnedUserId.ToString(), itemId);
            dbService.SetCurrentIcon(OwnedUserId, itemId);*/
        }
    }
}
