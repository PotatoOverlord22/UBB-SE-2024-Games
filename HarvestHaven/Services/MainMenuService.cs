using System.ComponentModel;
using System.Runtime.CompilerServices;
using HarvestHaven.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public class MainMenuService : ServiceBase, IMainMenuService
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

        public string WelcomeUserMessage
        {
            get
            {
                return $"Welcome, {UserName}!";
            }
        }

        public MainMenuService()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                UserName = user.Username;
            }
        }
    }
}
