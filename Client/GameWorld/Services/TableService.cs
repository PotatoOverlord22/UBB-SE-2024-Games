using GameWorld.Models;
using GameWorld.Resources.Utils;
using GameWorld.Views;
using Microsoft.Data.SqlClient;

namespace GameWorld.Services
{
    public class TableService : ITableService
    {
        private string tableType;
        private static Mutex mutex;
        private const int FULL = 8;
        private const int EMPTY = 0;
        private const int PREFLOP = 0;
        private const int FLOP = 1;
        private const int TURN = 2;
        private const int RIVER = 3;
        private const int INACTIVE = 0;
        private const int WAITING = 1;
        private const int FOLDED = 2;
        private const int PLAYING = 3;
        private const int FIRSTCARD = 1;
        private const int SECONDCARD = 2;
        private bool ended = false;
        private int buyIn;
        private int smallBlind;
        private int bigBlind;
        private List<MenuWindow> users;
        private CardDeck deck;
        private DataBaseService databaseService;
        private Random random;
        private HandRankCalculator rankCalculator;

        private PlayingCard[] communityCards;
        private int[] freeSpace;

        public TableService(int buyIn, int smallBlind, int bigBlind, string tableType, DataBaseService databaseService)
        {
            this.databaseService = databaseService;
            this.tableType = tableType;
            users = new List<MenuWindow>();
            rankCalculator = new HandRankCalculator();

            this.buyIn = buyIn;
            this.smallBlind = smallBlind;
            this.bigBlind = bigBlind;

            communityCards = new PlayingCard[6];
            freeSpace = new int[9];

            Task.Run(() => RunTable());

            mutex = new Mutex();

            random = new Random();
            this.databaseService = databaseService;
        }

        public PlayingCard GenerateCard()
        {
            int index = random.Next(0, deck.GetDeckSize());
            PlayingCard card = deck.GetCardFromIndex(index);
            deck.RemoveCardFromIndex(index);
            return card;
        }

        private void ReloadPlayerStackWithChips(User player)
        {
            player.UserChips -= buyIn;
            databaseService.UpdateUserChips(player.Id, player.UserChips);
            player.UserStack = buyIn;
            databaseService.UpdateUserStack(player.Id, player.UserStack);
        }

        private PlayingCard DealCard(User player, int index)
        {
            PlayingCard card = GenerateCard();
            player.UserCurrentHand[index] = card;

            return card;
        }

        private async void DealInitialCards(Queue<MenuWindow> activePlayers)
        {
            // deals the first card to all players
            foreach (MenuWindow playerWindow in activePlayers)
            {
                User player = playerWindow.Player();
                PlayingCard card = DealCard(player, 0);
                foreach (MenuWindow window in activePlayers)
                {
                    window.NotifyUserCard(tableType, player, FIRSTCARD, card.Value + card.Suit);
                }
                await Task.Delay(400);
            }

            // deals the second card to all players
            foreach (MenuWindow playerWindow in activePlayers)
            {
                User player = playerWindow.Player();
                PlayingCard card = DealCard(player, 1);
                foreach (MenuWindow window in activePlayers)
                {
                    window.NotifyUserCard(tableType, player, SECONDCARD, card.Value + card.Suit);
                }
                await Task.Delay(400);
            }

            GenerateAllCardsForTheBoard();
        }

        private void GenerateAllCardsForTheBoard()
        {
            for (int i = 1; i <= 5; i++)
            {
                PlayingCard card = GenerateCard();
                communityCards[i] = card;
            }
        }

        private void StartRoundForAllPlayers(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow currentWindow in activePlayers)
            {
                User currentPlayer = currentWindow.Player();
                foreach (MenuWindow otherWindow in activePlayers)
                {
                    User otherPlayer = otherWindow.Player();
                    currentWindow.StartRound(tableType, otherPlayer);
                }
            }
        }
        private void ResetCardsForAllPlayers(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow window in activePlayers)
            {
                window.ResetCards(tableType);
            }
        }
        private async void ShowFlopCards(Queue<MenuWindow> activePlayers)
        {
            for (int cardNumber = 1; cardNumber <= 3; cardNumber++)
            {
                foreach (MenuWindow window in activePlayers)
                {
                    window.NotifyTableCard(tableType, cardNumber, communityCards[cardNumber].CompleteInformation());
                }
                await Task.Delay(400);
            }
        }
        private async void ShowTurnCards(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow window in activePlayers)
            {
                window.NotifyTableCard(tableType, 4, communityCards[4].CompleteInformation());
            }
            await Task.Delay(400);
        }
        private async void ShowRiverCards(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow window in activePlayers)
            {
                window.NotifyTableCard(tableType, 5, communityCards[5].CompleteInformation());
            }
            await Task.Delay(400);
        }
        private void NotifyBetChangesToAllPlayers(Queue<MenuWindow> activePlayers, User player, int tablePot)
        {
            foreach (MenuWindow window in activePlayers)
            {
                window.Notify(tableType, player, tablePot);
            }
        }
        private void EndTurnOfAllPlayers(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow currentWindow in activePlayers)
            {
                User currentPlayer = currentWindow.Player();
                foreach (MenuWindow otherWindow in activePlayers)
                {
                    User otherPlayer = otherWindow.Player();
                    currentWindow.EndTurn(tableType, otherPlayer);
                }
                currentPlayer.UserBet = 0;
            }
        }
        private void ShowTheCardsOfAllPlayers(Queue<MenuWindow> activePlayers)
        {
            foreach (MenuWindow currentWindow in activePlayers)
            {
                User currentPlayer = currentWindow.Player();
                foreach (MenuWindow otherWindow in activePlayers)
                {
                    User otherPlayer = otherWindow.Player();
                    currentWindow.ShowCards(tableType, otherPlayer);
                }
            }
        }
        private void FoldPlayer(User player, Queue<MenuWindow> activePlayers)
        {
            Console.WriteLine(player.Username + " folded!");
            player.UserStatus = WAITING;
            player.UserBet = 0;
            foreach (MenuWindow window in activePlayers)
            {
                window.FoldPlayer(tableType, player);
            }
        }
        private async void DisplayWinner(Queue<MenuWindow> activePlayers, User winner)
        {
            for (int i = 1; i <= 6; i++)
            {
                foreach (MenuWindow menuWindow in activePlayers)
                {
                    if (i % 2 == 1)
                    {
                        menuWindow.DisplayWinner(tableType, winner, true);
                    }
                    else
                    {
                        menuWindow.DisplayWinner(tableType, winner, false);
                    }
                }
                if (i == 5)
                {
                    await Task.Delay(2000);
                }
                else
                {
                    await Task.Delay(200);
                }
            }
        }
        private void PreparePlayersForRound(Queue<MenuWindow> allActivePlayers)
        {
            foreach (MenuWindow menuWindow in allActivePlayers)
            {
                User player = menuWindow.Player();
                player.UserStatus = player.UserStack == 0 ? INACTIVE : PLAYING;

                if (player.UserStatus == INACTIVE)
                {
                    if (player.UserChips < buyIn)
                    {
                        DisconnectUser(menuWindow);
                    }
                    else
                    {
                        ReloadPlayerStackWithChips(player);
                        menuWindow.UpdateChips(tableType, player);
                    }
                }
                else
                {
                    player.UserBet = 0;
                }
            }
        }

        public async void RunTable()
        {
            while (!ended)
            {
                if (IsEmpty())
                {
                    await Task.Delay(3000);
                    continue;
                }

                mutex.WaitOne();
                Queue<MenuWindow> allActivePlayers = new Queue<MenuWindow>(users);
                mutex.ReleaseMutex();

                PreparePlayersForRound(allActivePlayers);
                StartRoundForAllPlayers(allActivePlayers);
                ResetCardsForAllPlayers(allActivePlayers);

                if (allActivePlayers.Count < 2)
                {
                    await Task.Delay(5000);
                    continue;
                }
                deck = new CardDeck();
                await Task.Delay(1000);

                DealInitialCards(allActivePlayers);

                int tablePot = 0;
                int numberOfPlayersThatCanBet = allActivePlayers.Count;
                for (int turn = PREFLOP; turn <= RIVER; turn++)
                {
                    if (allActivePlayers.Count < 2)
                    {
                        break;
                    }

                    if (turn == FLOP)
                    {
                        ShowFlopCards(allActivePlayers);
                    }
                    else if (turn == TURN)
                    {
                        ShowTurnCards(allActivePlayers);
                    }
                    else if (turn == RIVER)
                    {
                        ShowRiverCards(allActivePlayers);
                    }

                    int currentBet = -1;
                    Guid idOfPlayerWithMaxBet = Guid.Empty;

                    while (numberOfPlayersThatCanBet >= 2)
                    {
                        MenuWindow currentWindow = allActivePlayers.Peek();
                        User player = currentWindow.Player();

                        if (player.UserStatus != PLAYING)
                        {
                            allActivePlayers.Dequeue();
                            continue;
                        }

                        if (player.Id == idOfPlayerWithMaxBet)
                        {
                            break;
                        }

                        if (player.UserStack == 0)
                        {
                            numberOfPlayersThatCanBet--;
                            allActivePlayers.Enqueue(allActivePlayers.Dequeue());
                            continue;
                        }

                        int playerBet = await currentWindow.StartTime(tableType, Math.Min(currentBet, player.UserStack + player.UserBet), player.UserStack + player.UserBet);

                        if (playerBet == -1)
                        {
                            FoldPlayer(player, allActivePlayers);
                            allActivePlayers.Dequeue();
                            numberOfPlayersThatCanBet--;
                        }
                        else
                        {
                            allActivePlayers.Enqueue(allActivePlayers.Dequeue());
                            int extraBet = playerBet - player.UserBet;
                            player.UserStack -= extraBet;
                            tablePot += extraBet;
                            databaseService.UpdateUserStack(player.Id, player.UserStack);
                            player.UserBet = playerBet;

                            if (playerBet > currentBet)
                            {
                                currentBet = playerBet;
                                idOfPlayerWithMaxBet = player.Id;
                            }
                        }

                        NotifyBetChangesToAllPlayers(allActivePlayers, player, tablePot);
                    }
                    EndTurnOfAllPlayers(allActivePlayers);
                }
                ShowTheCardsOfAllPlayers(allActivePlayers);

                List<User> winners = DetermineWinners(allActivePlayers);
                foreach (User winner in winners)
                {
                    Console.WriteLine("Winner: " + winner.Username);
                    winner.UserStack += Convert.ToInt32(tablePot / winners.Count);
                    databaseService.UpdateUserStack(winner.Id, winner.UserStack);
                    DisplayWinner(allActivePlayers, winner);
                }
                await Task.Delay(3000);
            }
        }

        private static void GenerateHands(List<PlayingCard> currentHand, List<PlayingCard> possibleCards, int lastCard, int numberCards, List<List<PlayingCard>> allHands)
        {
            for (int i = lastCard + 1; i < possibleCards.Count; i++)
            {
                currentHand.Add(possibleCards[i]);
                if (currentHand.Count == numberCards)
                {
                    List<PlayingCard> handCopy = new List<PlayingCard>(currentHand);
                    allHands.Add(handCopy);
                }
                else
                {
                    GenerateHands(currentHand, possibleCards, i, numberCards, allHands);
                }
                currentHand.Remove(possibleCards[i]);
            }
        }

        public Tuple<int, int> DetermineMaxHand(List<PlayingCard> possibleCards)
        {
            Tuple<int, int> maxHandValue = new Tuple<int, int>(0, 0);
            List<List<PlayingCard>> allHands = new List<List<PlayingCard>>();
            List<PlayingCard> currentHand = new List<PlayingCard>();
            GenerateHands(currentHand, possibleCards, -1, 5, allHands);
            // Console.WriteLine("Generated hands: " + allHands.Count);
            foreach (List<PlayingCard> hand in allHands)
            {
                Tuple<int, int> handValue = rankCalculator.GetValue(hand);
                if (handValue.Item1 > maxHandValue.Item1 || (handValue.Item1 == maxHandValue.Item1 && handValue.Item2 > maxHandValue.Item2))
                {
                    maxHandValue = handValue;
                }
            }
            // Console.WriteLine("Best hand: " + maxHandValue);
            return maxHandValue;
        }

        public List<User> DetermineWinners(Queue<MenuWindow> activePlayers)
        {
            Tuple<int, int> maxHand = new Tuple<int, int>(0, 0);
            List<User> winners = new List<User>();
            Dictionary<User, Tuple<int, int>> results = new Dictionary<User, Tuple<int, int>>();
            foreach (MenuWindow window in activePlayers)
            {
                User player = window.Player();
                List<PlayingCard> possibleCards = new List<PlayingCard>();
                for (int i = 1; i <= 5; i++)
                {
                    possibleCards.Add(communityCards[i]);
                }
                possibleCards.Add(player.UserCurrentHand[0]);
                possibleCards.Add(player.UserCurrentHand[1]);
                Tuple<int, int> hand = DetermineMaxHand(possibleCards);
                if (hand.Item1 > maxHand.Item1 || (hand.Item1 == maxHand.Item1 && hand.Item2 > maxHand.Item2))
                {
                    maxHand = hand;
                    winners.Clear();
                    winners.Add(player);
                }
                else if (hand.Item1 == maxHand.Item1 && hand.Item2 == maxHand.Item2)
                {
                    winners.Add(player);
                }
            }
            return winners;
        }

        public void DisconnectUser(MenuWindow window)
        {
            User player = window.Player();
            freeSpace[player.UserTablePlace] = 0;
            mutex.WaitOne();
            foreach (MenuWindow windowUser in users)
            {
                windowUser.FoldPlayer(tableType, player);
                windowUser.HidePlayer(tableType, player);
            }
            users.Remove(window);
            mutex.ReleaseMutex();
        }

        public int JoinTable(MenuWindow window, ref SqlConnection sqlConnection)
        {
            if (IsFull())
            {
                return 0;
            }

            User player = window.Player();

            if (player.UserChips < buyIn)
            {
                return -1;
            }

            player.UserChips -= buyIn;
            databaseService.UpdateUserChips(player.Id, player.UserChips);

            player.UserStack = buyIn;
            databaseService.UpdateUserStack(player.Id, player.UserStack);

            player.UserStatus = WAITING;
            for (int i = 1; i <= FULL; i++)
            {
                if (freeSpace[i] == 0)
                {
                    freeSpace[i] = 1;
                    player.UserTablePlace = i;
                    break;
                }
            }
            mutex.WaitOne();
            users.Add(window);
            foreach (MenuWindow windowUser in users)
            {
                windowUser.ShowPlayer(tableType, player);
                window.ShowPlayer(tableType, windowUser.Player());
            }
            mutex.ReleaseMutex();

            return 1;
        }

        public bool IsFull()
        {
            return users.Count == FULL;
        }

        public bool IsEmpty()
        {
            return users.Count == EMPTY;
        }

        public int Occupied()
        {
            return users.Count;
        }
    }
}
