using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;
namespace GameWorld.Views
{
    public partial class GameBoardWindow : UserControl
    {
        private int leftDiceValue = 0;
        private int rightDiceValue = 0;
        private UIElement leftDice;
        private UIElement rightDice;
        private int currentPlayerTries = 2;
        private ISkillIssueBroService skillIssueBroService;
        // temporary hardcoded players
        private List<Player> players = new List<Player>
        {
            new Player("Egg"),
            new Player("Mario"),
            new Player("Gigi"),
            new Player("Flower")
        };

        public GameBoardWindow(ISkillIssueBroService skillIssueBroService)
        {
            InitializeComponent();
            Loaded += GameBoardWindow_Loaded;
            column1.rollButton.ButtonClicked += RollButton_Clicked;
            gameBoard.PawnClicked += OnPawnClicked;
            this.skillIssueBroService = skillIssueBroService;
        }

        private async void OnPawnKilled(object sender)
        {
            try
            {
                List<Pawn> pawns = await skillIssueBroService.GetPawns();
                SpawnPawns(pawns);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ShowCurrentPlayerColorEllipseAsync()
        {
            try
            {
                string playerColorString = await skillIssueBroService.GetCurrentPlayerColor();
                Color playerColor = StringToColor(playerColorString);

                var ellipse = new Ellipse();
                Brush brush = new SolidColorBrush(playerColor);
                ellipse.Fill = brush;
                ellipse.Height = 100;
                ellipse.Width = 100;
                column2.column2Grid.Children.Add(ellipse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Color StringToColor(string colorString)
        {
            switch (colorString)
            {
                case "b":
                    return Colors.Blue;
                case "y":
                    return Colors.Yellow;
                case "g":
                    return Colors.Green;
                case "r":
                    return Colors.Red;
                default:
                    return Colors.White;
            }
        }

        private void HideDice()
        {
            column1.column1Grid.Children.Remove(leftDice);
            column2.column2Grid.Children.Remove(rightDice);
        }

        private async void RerollDice()
        {
            leftDiceValue = 0;
            rightDiceValue = 0;

            ClearPawnChildren();
            List<Pawn> pawns = await skillIssueBroService.GetPawns();
            SpawnPawns(pawns);
            HideDice();
            column1.column1Grid.Children[1].Visibility = Visibility.Visible;
        }

        private async Task SwitchToNextTurnAsync()
        {
            leftDiceValue = 0;
            rightDiceValue = 0;
            currentPlayerTries = 2;

            ClearPawnChildren();
            try
            {
                List<Pawn> pawns = await skillIssueBroService.GetPawns();
                SpawnPawns(pawns);
                skillIssueBroService.NextPlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            await ShowCurrentPlayerColorEllipseAsync();
            HideDice();
            column1.column1Grid.Children[1].Visibility = Visibility.Visible;
        }

        private async void OnPawnClicked(object sender, PawnClickedEventArgs e)
        {
            int column = e.Column;
            int row = e.Row;

            try
            {
                skillIssueBroService.MovePawnBasedOnClick(column, row, leftDiceValue, rightDiceValue);
                if (leftDiceValue != rightDiceValue)
                {
                    await SwitchToNextTurnAsync();
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
                        await SwitchToNextTurnAsync();
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
                        await SwitchToNextTurnAsync();
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + "\nTries left 1");
                    }
                }
            }
        }

        private async void RollButton_Clicked(object sender, EventArgs e)
        {
            // roll twice
            try
            {
                leftDiceValue = await skillIssueBroService.RollDice();
                rightDiceValue = await skillIssueBroService.RollDice();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private async void GameBoardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Pawn> pawns = await skillIssueBroService.GetPawns();
                SpawnPawns(pawns);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            await ShowCurrentPlayerColorEllipseAsync();

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
