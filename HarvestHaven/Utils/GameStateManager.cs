using System;
using HarvestHaven.Entities;

namespace HarvestHaven.Utils
{
    public static class GameStateManager
    {
        private static User? _currentUser;

        public static User? GetCurrentUser()
        {
            return _currentUser;
        }

        public static Guid GetCurrentUserId()
        {
            return _currentUser.Id;
        }

        public static void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
