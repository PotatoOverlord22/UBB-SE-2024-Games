using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;
using SuperbetBeclean.ViewModels;

namespace SuperbetBeclean.Models
{
    internal class ProfileViewModel
    {
        private IDataBaseService databaseService;

        private MenuWindow mainWindow;
        public List<ShopItem> OwnedItems { get; set; }

        public ProfileViewModel(MenuWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            OwnedItems = new List<ShopItem>();
            databaseService = new DataBaseService();
            LoadItems();
        }

        private void LoadItems()
        {
            List<ShopItem> ownedItems = databaseService.GetAllUserIconsByUserId(mainWindow.UserId());
            foreach (var item in ownedItems)
            {
                item.UserId = mainWindow.UserId();
                OwnedItems.Add(item);
            }
        }
    }
}
