using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;
using GameWorldClassLibrary.Utils;

namespace GameWorld.Services
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
