using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;
using GameWorldClassLibrary.Utils;

namespace GameWorldClassLibrary.Services
{
    public class HarvestHavenMainService : ServiceBase, IHarvestHavenMainService
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

        public HarvestHavenMainService()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null)
            {
                UserName = user.Username;
            }
        }
    }
}
