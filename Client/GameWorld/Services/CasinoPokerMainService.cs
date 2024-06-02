using System.Windows;
using GameWorldClassLibrary.Models;
using GameWorld.Resources.Utils;
using GameWorld.Views;
using Microsoft.Data.SqlClient;

namespace GameWorld.Services
{
    public class CasinoPokerMainService : ICasinoPokerMainService
    {
        private List<MenuWindow> openedUsersWindows;
        private SqlConnection sqlConnection;
        private IUserService userService;
        private const int FULL = 8;
        private const int EMPTY = 0;
        private const int INACTIVE = 0;
        private const int WAITING = 1;
        private const int PLAYING = 2;
        private const string JUNIOR = "junior";
        private const string INTERN = "intern";
        private const string SENIOR = "senior";
        private ChatWindow chatWindowIntern;
        private ChatWindow chatWindowJuniorm;
        private ChatWindow chatWindowSenior;
        private TableService internTable;
        private TableService juniorTable;
        private TableService seniorTable;
        private string connectionString;

        private const int INTERN_BUY_IN = 500;
        private const int JUNIOR_BUY_IN = 5000;
        private const int SENIOR_BUY_IN = 50000;

        private const int INTERN_SMALL_BLIND = 50;
        private const int INTERN_BIG_BLIND = 100;

        private const int JUNIOR_SMALL_BLIND = 500;
        private const int JUNIOR_BIG_BLIND = 1000;

        private const int SENIOR_SMALL_BLIND = 5000;
        private const int SENIOR_BIG_BLIND = 10000;

        private const int DAILY_LOGIN_STREAK_MULTIPLIER = 5000;

        private const int INITIAL_STREAK = 1;
        private const int DAYS_BETWEEN_LOGIN_BONUSES = 1;

        // Task internTask, juniorTask, seniorTask;
        public CasinoPokerMainService(IUserService userService)
        {
            this.userService = userService;
            openedUsersWindows = new List<MenuWindow>();
            internTable = new TableService(INTERN_BUY_IN, INTERN_SMALL_BLIND, INTERN_BIG_BLIND, INTERN, userService);
            juniorTable = new TableService(JUNIOR_BUY_IN, JUNIOR_SMALL_BLIND, JUNIOR_BIG_BLIND, JUNIOR, userService);
            seniorTable = new TableService(SENIOR_BUY_IN, SENIOR_SMALL_BLIND, SENIOR_BIG_BLIND, SENIOR, userService);
            // chatWindowIntern = new ChatWindow();
            // chatWindowJuniorm = new ChatWindow();
            // chatWindowSenior = new ChatWindow();
        }

        public int OccupiedIntern()
        {
            return internTable.Occupied();
        }

        public int OccupiedJunior()
        {
            return juniorTable.Occupied();
        }

        public int OccupiedSenior()
        {
            return seniorTable.Occupied();
        }

        public void NewUserLogin(User newUser)
        {
            Console.WriteLine("New user login");
            if (DateTime.Now.Date != newUser.UserLastLogin.Date)
            {
                var diffDates = DateTime.Now.Date - newUser.UserLastLogin.Date;
                if (diffDates.Days == DAYS_BETWEEN_LOGIN_BONUSES)
                {
                    newUser.UserStreak++;
                }
                else
                {
                    newUser.UserStreak = INITIAL_STREAK;
                }
                newUser.UserChips += newUser.UserStreak * DAILY_LOGIN_STREAK_MULTIPLIER;
                userService.UpdateUserChips(newUser.Id, newUser.UserChips);
                userService.UpdateUserStreak(newUser.Id, newUser.UserStreak);
                MessageBox.Show("Congratulations, you got your daily bonus!\n" + "Streak: " + newUser.UserStreak + " Bonus: " + (DAILY_LOGIN_STREAK_MULTIPLIER * newUser.UserStreak).ToString());
            }
            userService.UpdateUserLastLogin(newUser.Id, DateTime.Now);
        }
        private void OpenUserWindow(User user)
        {
            Console.WriteLine("Open user window");
            MenuWindow menuWindow = new MenuWindow(user, this);
            menuWindow.Show();
            openedUsersWindows.Add(menuWindow);
        }
        public void AddWindow(string username)
        {
            User user = GameStateManager.GetCurrentUser();
            try
            {
                if (user != null)
                {
                    OpenUserWindow(user);
                    NewUserLogin(user);
                }
                else
                {
                    MessageBox.Show("The username is not valid.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void DisconnectUser(MenuWindow window)
        {
            User player = window.Player();
            player.UserStatus = INACTIVE;
            player.UserBet = 0;
            internTable.DisconnectUser(window);
            juniorTable.DisconnectUser(window);
            seniorTable.DisconnectUser(window);

            player.UserChips += player.UserStack;
            userService.UpdateUserChips(player.Id, player.UserChips);
            player.UserStack = EMPTY;
            userService.UpdateUserStack(player.Id, player.UserStack);
        }
        public int JoinInternTable(MenuWindow window)
        {
            return internTable.JoinTable(window, ref sqlConnection);
        }

        public int JoinJuniorTable(MenuWindow window)
        {
            return juniorTable.JoinTable(window, ref sqlConnection);
        }

        public int JoinSeniorTable(MenuWindow window)
        {
            return seniorTable.JoinTable(window, ref sqlConnection);
        }
    }
}
