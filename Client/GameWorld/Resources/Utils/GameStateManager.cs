using System;
using GameWorldClassLibrary.Models;

namespace GameWorld.Resources.Utils
{
    public static class GameStateManager
    {
        private static User? currentUser;

        public static User? GetCurrentUser()
        {
            return currentUser;
        }

        public static Guid GetCurrentUserId()
        {
            return currentUser.Id;
        }

        public static void SetCurrentUser(User user)
        {
            currentUser = user;
        }
    }
}
