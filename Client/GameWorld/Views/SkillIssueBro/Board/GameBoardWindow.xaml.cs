using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameWorldClassLibrary.Models;
namespace GameWorld.Views
{
    public partial class GameBoardWindow : UserControl
    {
        private int leftDiceValue = 0;
        private int rightDiceValue = 0;
        private UIElement leftDice;
        private UIElement rightDice;
        private int currentPlayerTries = 2;
        // temporary hardcoded players
        private List<Player> players = new List<Player>
        {
            new Player("Egg"),
            new Player("Mario"),
            new Player("Gigi"),
            new Player("Flower")
        };

        public GameBoardWindow()
        {
            InitializeComponent();
            Loaded += GameBoardWindow_Loaded;
            column1.rollButton.ButtonClicked += RollButton_Clicked;
            gameBoard.PawnClicked += OnPawnClicked;
        }

        private void OnPawnKilled(object sender)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // get paws and spawn them again
                var response = client.GetAsync("api/SkillIssueBroGame/GetPawns");
                if (response.Result.IsSuccessStatusCode)
                {
                    var pawnList = response.Result.Content.ReadFromJsonAsync<List<Pawn>>().Result;
                    SpawnPawns(pawnList);
                }
            }
        }

        private async Task ShowCurrentPlayerColorEllipseAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/SkillIssueBroGame/GetCurrentPlayerColor");
                if (response.IsSuccessStatusCode)
                {
                    string color = await response.Content.ReadAsStringAsync();
                    color = color.Replace("\"", string.Empty);
                    var ellipse = new Ellipse();
                    Color playerColor;
                    switch (color)
                    {
                        case "b":
                            playerColor = Colors.Blue; break;
                        case "y":
                            playerColor = Colors.Yellow; break;
                        case "g":
                            playerColor = Colors.Green; break;
                        case "r":
                            playerColor = Colors.Red; break;
                        default:
                            playerColor = Colors.White; break;
                    }
                    Brush brush = new SolidColorBrush(playerColor);
                    ellipse.Fill = brush;
                    ellipse.Height = 100;
                    ellipse.Width = 100;
                    column2.column2Grid.Children.Add(ellipse);
                }
            }
        }

        private void HideDice()
        {
            column1.column1Grid.Children.Remove(leftDice);
            column2.column2Grid.Children.Remove(rightDice);
        }

        private void RerollDice()
        {
            leftDiceValue = 0;
            rightDiceValue = 0;

            // this is not business logic, just that after every turn, they cleared the whole
            // grid and spawned the pawns again with their new positions
            ClearPawnChildren();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // get paws and spawn them again
                var response = client.GetAsync("api/SkillIssueBroGame/GetPawns");
                if (response.Result.IsSuccessStatusCode)
                {
                    var pawnList = response.Result.Content.ReadFromJsonAsync<List<Pawn>>().Result;
                    SpawnPawns(pawnList);
                }
            }
            HideDice();
            column1.column1Grid.Children[1].Visibility = Visibility.Visible;
        }

        private async Task SwitchToNextTurnAsync()
        {
            leftDiceValue = 0;
            rightDiceValue = 0;
            currentPlayerTries = 2;

            ClearPawnChildren();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // get paws and spawn them again
                var response = client.GetAsync("api/SkillIssueBroGame/GetPawns");
                if (response.Result.IsSuccessStatusCode)
                {
                    var pawnList = response.Result.Content.ReadFromJsonAsync<List<Pawn>>().Result;
                    SpawnPawns(pawnList);
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("api/SkillIssueBroGame/ChangeCurrentPlayer");
            }
            ShowCurrentPlayerColorEllipseAsync();
            HideDice();
            column1.column1Grid.Children[1].Visibility = Visibility.Visible;
        }

        private void OnPawnClicked(object sender, PawnClickedEventArgs e)
        {
            int column = e.Column;
            int row = e.Row;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5070/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync($"api/SkillIssueBroGame/MovePawnBasedOnClick?column={column}&row={row}&leftDiceValue={leftDiceValue}&rightDiceValue={rightDiceValue}").Result;
                    if (!response.Content.ReadAsStringAsync().Result.Equals("Moved pawn successfully"))
                    {
                        throw new Exception(response.Content.ReadAsStringAsync().Result);
                    }
                }
                if (leftDiceValue != rightDiceValue)
                {
                    SwitchToNextTurnAsync();
                }
                else
                {
                    RerollDice();
                }
}
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\"", string.Empty);
                if (message.Equals("Can't move pawn yet"))
                {
                    MessageBox.Show(ex.Message);
                }
                else if (message.Equals("You have to roll two 6s!"))
                {
                    MessageBox.Show(ex.Message);
                    if (leftDiceValue != rightDiceValue)
                    {
                        SwitchToNextTurnAsync();
                    }
                    else
                    {
                        RerollDice();
                    }
                }
                else
                {
                    // penalize player to hurry game
                    currentPlayerTries--;
                    if (currentPlayerTries == 0)
                    {
                        MessageBox.Show("Tries left 0\nSkipping turn");
                        SwitchToNextTurnAsync();
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + "\nTries left 1");
                    }
                }
            }
        }

        private void RollButton_Clicked(object sender, EventArgs e)
        {
            // request the controller to roll the dice
            // will need to make 2 requests to get the values of the dice
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/SkillIssueBroGame/RollDice");
                if (response.Result.IsSuccessStatusCode)
                {
                    leftDiceValue = response.Result.Content.ReadFromJsonAsync<int>().Result;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/SkillIssueBroGame/RollDice");
                if (response.Result.IsSuccessStatusCode)
                {
                    rightDiceValue = response.Result.Content.ReadFromJsonAsync<int>().Result;
                }
            }
            // show the dice in the view
            GenerateLeftDice(leftDiceValue);
            GenerateRightDice(rightDiceValue);
        }

        private void GenerateLeftDice(int value)
        {
            switch (value)
            {
                case 1:
                    leftDice = new DiceWithNumber1();
                    break;
                case 2:
                    leftDice = new DiceWithNumber2();
                    break;
                case 3:
                    leftDice = new DiceWithNumber3();
                    break;
                case 4:
                    leftDice = new DiceWithNumber4();
                    break;
                case 5:
                    leftDice = new DiceWithNumber5();
                    break;
                default:
                    leftDice = new DiceWithNumber6();
                    break;
            }

            // Add the leftDice to the grid and store the reference in the dictionary
            Grid.SetColumn(leftDice, 0);
            Grid.SetRow(leftDice, 1);
            column1.column1Grid.Children.Add(leftDice);
        }

        private void GenerateRightDice(int value)
        {
            switch (value)
            {
                case 1:
                    rightDice = new DiceWithNumber1();
                    break;
                case 2:
                    rightDice = new DiceWithNumber2();
                    break;
                case 3:
                    rightDice = new DiceWithNumber3();
                    break;
                case 4:
                    rightDice = new DiceWithNumber4();
                    break;
                case 5:
                    rightDice = new DiceWithNumber5();
                    break;
                default:
                    rightDice = new DiceWithNumber6();
                    break;
            }

            Grid.SetColumn(rightDice, 0);
            Grid.SetRow(rightDice, 1);
            column2.column2Grid.Children.Add(rightDice);
        }

        private void GameBoardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // create the controller when the window is loaded
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5070/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // get paws and spawn them again
                var response = client.GetAsync("api/SkillIssueBroGame/GetPawns");
                if (response.Result.IsSuccessStatusCode)
                {
                    var pawnList = response.Result.Content.ReadFromJsonAsync<List<Pawn>>().Result;
                    SpawnPawns(pawnList);
                }
            }
            ShowCurrentPlayerColorEllipseAsync();

            // skillIssueBroController.PawnKilled += OnPawnKilled;
        }

        private void SpawnPawns(List<Pawn> pawnsToSpawn)
        {
            // blue and yellow spawned regardless
            for (int i = 0; i < 4; i++)
            {
                Tile occupiedTile = pawnsToSpawn[i].GetOccupiedTile();
                gameBoard.AddBluePawn((int)occupiedTile.GetCenterXPosition(), (int)occupiedTile.GetCenterYPosition());
            }

            for (int i = 4; i < 8; i++)
            {
                Tile occupiedTile = pawnsToSpawn[i].GetOccupiedTile();
                gameBoard.AddYellowPawn((int)occupiedTile.GetCenterXPosition(), (int)occupiedTile.GetCenterYPosition());
            }

            // green if player count > 2
            if (players.Count > 2)
            {
                for (int i = 8; i < 12; i++)
                {
                    Tile occupiedTile = pawnsToSpawn[i].GetOccupiedTile();
                    gameBoard.AddGreenPawn((int)occupiedTile.GetCenterXPosition(), (int)occupiedTile.GetCenterYPosition());
                }
            }
            // red if player count > 3
            if (players.Count > 3)
            {
                for (int i = 12; i < 16; i++)
                {
                    Tile occupiedTile = pawnsToSpawn[i].GetOccupiedTile();
                    gameBoard.AddRedPawn((int)occupiedTile.GetCenterXPosition(), (int)occupiedTile.GetCenterYPosition());
                }
            }
        }

        private void ClearPawnChildren()
        {
            for (int i = gameBoard.MainGrid.Children.Count - 1; i >= 0; i--)
            {
                if (gameBoard.MainGrid.Children[i] is PawnBlue ||
                    gameBoard.MainGrid.Children[i] is PawnYellow ||
                    gameBoard.MainGrid.Children[i] is PawnGreen ||
                    gameBoard.MainGrid.Children[i] is PawnRed)
                {
                    gameBoard.MainGrid.Children.RemoveAt(i);
                }
            }
        }
    }
}
