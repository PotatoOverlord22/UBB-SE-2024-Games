using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarvestHaven.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public class MainMenuService : IMainMenuService, INotifyPropertyChanged
    {
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMenuService()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                UserName = "Welcome, " + user.Username;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
