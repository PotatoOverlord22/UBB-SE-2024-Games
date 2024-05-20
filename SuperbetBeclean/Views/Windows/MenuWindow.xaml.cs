using System.Windows;
using SuperbetBeclean.Model;
using SuperbetBeclean.Pages;
using SuperbetBeclean.Services;

namespace SuperbetBeclean.Windows
{
    public partial class MenuWindow : Window
    {
        private User user;
        private IMainService service;
        private Dictionary<string, GameTablePage> gamePages;

        public MenuWindow(User user, MainService service)
        {
            InitializeComponent();
            this.service = service;
            this.user = user;
            this.Title = this.user.UserName;
            MenuFrame.Navigate(new MainMenu(MenuFrame, this, service, this.user));
            gamePages = new Dictionary<string, GameTablePage>();
            gamePages.Add("intern", new GameTablePage(MenuFrame, this, this.service, "intern"));
            gamePages.Add("junior", new GameTablePage(MenuFrame, this, this.service, "junior"));
            gamePages.Add("senior", new GameTablePage(MenuFrame, this, this.service, "senior"));
            Closed += DisconnectUser;
        }
        public void DisconnectUser(object sender, System.EventArgs systemEvent)
        {
            service.DisconnectUser(this);
        }

        async public Task<int> StartTime(string table, int minBet, int maxBet)
        {
            int bet = 0;
            bet = await gamePages[table].RunTimer(minBet, maxBet);
            return bet;
        }

        public void UpdateChips(string table, User player)
        {
            gamePages[table].UpdateChips(player);
        }
        public void ResetCards(string table)
        {
            gamePages[table].ResetCards();
        }

        public void NotifyUserCard(string table, User u, int card, string cardValue)
        {
            gamePages[table].AddUserCard(u == user, u.UserTablePlace, card, cardValue);
        }

        public void NotifyTableCard(string table, int card, string cardValue)
        {
            gamePages[table].AddTableCard(card, cardValue);
        }

        public void ShowCards(string table, User player)
        {
            gamePages[table].ShowCards(player);
        }
        public void Notify(string table, User player, int tablePot)
        {
            gamePages[table].UpdatePlayerMoney(player);
            gamePages[table].UpdatePot(tablePot);
        }
        public void FoldPlayer(string table, User player)
        {
            gamePages[table].PlayerFold(player);
        }
        public void ShowPlayer(string table, User player)
        {
            gamePages[table].ShowPlayer(player);
        }

        public void HidePlayer(string table, User player)
        {
            gamePages[table].HidePlayer(player);
        }
        public void EndTurn(string table, User player)
        {
            gamePages[table].EndTurn(player);
        }
        public void StartRound(string table, User player)
        {
            gamePages[table].StartRound(player);
        }
        public void ResetPot(string table)
        {
            gamePages[table].ResetPot();
        }
        public void DisplayWinner(string table, User player, bool status)
        {
            gamePages[table].DisplayWinner(player, status);
        }
        public User Player()
        {
            return user;
        }

        public string UserName()
        {
            return user.UserName;
        }

        public int UserLevel()
        {
            return user.UserLevel;
        }

        public int UserChips()
        {
            return user.UserChips;
        }

        public int UserStreak()
        {
            return user.UserStreak;
        }

        public string UserIcon()
        {
            return user.UserCurrentIconPath;
        }
        public int UserId()
        {
            return user.UserID;
        }
        public GameTablePage InternPage()
        {
            return gamePages["intern"];
        }
        public GameTablePage JuniorPage()
        {
            return gamePages["junior"];
        }
        public GameTablePage SeniorPage()
        {
            return gamePages["senior"];
        }
    }
}
