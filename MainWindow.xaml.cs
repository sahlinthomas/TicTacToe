using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
  
    public partial class MainWindow : Window
    {
        private bool isUserTurn = true;
        private List<Button> buttons;
        public MainWindow()
        {
            InitializeComponent();

            buttons = new List<Button> { TopLeft, TopMid, TopRight, MidLeft, MidMid, MidRight, BottomLeft, BottomMid, BottomRight };
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null && clickedButton.Background == Brushes.LightGray)
            {
                if (isUserTurn)
                {
                    clickedButton.Background = Brushes.LightGreen;
                    clickedButton.Content = "X";
                    clickedButton.FontSize = 48;

                }
                if (CheckForWin("X"))
                {
                    MessageBox.Show("You win!");
                    ResetGame();
                    return;
                }
                if (IsDraw())
                {
                    MessageBox.Show("It's a draw.");
                    ResetGame();
                    return;
                }
            }
            isUserTurn = false;
            OpponentMove();


        }
        private void OpponentMove()
        {
            var availableButtons = buttons.Where(b => b.Background == Brushes.LightGray).ToList();

            if (availableButtons.Any())
            {
                Random random = new Random();
                int randomIndex = random.Next(0, availableButtons.Count);
                Button selectedButton = availableButtons[randomIndex];

                selectedButton.Background = Brushes.Gold;
                selectedButton.Content = "O";
                selectedButton.FontSize = 48;

                if (CheckForWin("O"))
                {
                    MessageBox.Show("Opponent wins!");
                    ResetGame();
                    return;
                }
                isUserTurn = true;
            }
            else
            {
                MessageBox.Show("It's a draw.");
                ResetGame();
            }
        }
        private bool CheckForWin(string symbol)
        {
            var winningCombinations = new List<(Button, Button, Button)>
            {
                 (TopLeft, TopMid, TopRight),
                 (MidLeft, MidMid, MidRight),
                 (BottomLeft, BottomMid, BottomRight),
                 (TopLeft, MidLeft, BottomLeft),
                 (TopMid, MidMid, BottomMid),
                 (TopRight, MidRight, BottomRight),
                 (TopLeft, MidMid, BottomRight),
                 (TopRight, MidMid, BottomLeft)
            };

            foreach (var (b1, b2, b3) in winningCombinations)
            {
                if (b1.Content?.ToString() == symbol &&
                    b2.Content?.ToString() == symbol &&
                    b3.Content?.ToString() == symbol)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsDraw()
        {
            return buttons.All(b => b.Background != Brushes.LightGray) && !CheckForWin("X") && !CheckForWin("O");
        }
        private void ResetGame()
        {
            foreach (var button in buttons)
            {
                button.Content = null;
                button.Background = Brushes.LightGray;
            }
            isUserTurn = true;
        }

    }
}