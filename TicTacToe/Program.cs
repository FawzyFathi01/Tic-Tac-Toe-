using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the game
            TicTacToeGame game = new TicTacToeGame();

            // Start the game
            game.Start();

            // Keep the console window open after the game ends
            Console.ReadLine();
        }
    }

    public class TicTacToeGame
    {
        private char[,] board; // Represents the Tic Tac Toe board
        private char currentPlayer; // Represents the current player ('X' or 'O')
        private string player1Name;
        private string player2Name;
        private char player1Symbol;
        private char player2Symbol;
        private bool isAgainstComputer; // Indicates if the game is against the computer

        public TicTacToeGame()
        {
            // Initialize the board
            board = new char[3, 3];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Fill the board with numbers from 1 to 9
            char number = '1';
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = number++;
                }
            }
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");

            bool playAgain = true;
            while (playAgain)
            {
                // Choose game mode
                ChooseGameMode();

                // Get player names and symbols
                Console.Write("Enter Player 1's name: ");
                player1Name = Console.ReadLine();
                Console.Write("Choose Player 1's symbol (X/O): ");
                player1Symbol = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (!isAgainstComputer)
                {
                    Console.Write("Enter Player 2's name: ");
                    player2Name = Console.ReadLine();
                    player2Symbol = (player1Symbol == 'X') ? 'O' : 'X';
                }
                else
                {
                    player2Name = "Computer";
                    player2Symbol = (player1Symbol == 'X') ? 'O' : 'X';
                }

                // Play the game
                PlayGame();

                // Ask if players want to play again
                playAgain = AskToPlayAgain();
            }

            Console.WriteLine("Thank you for playing!");
        }

        private void ChooseGameMode()
        {
            Console.WriteLine("Choose game mode:");
            Console.WriteLine("1. Two Players");
            Console.WriteLine("2. Against Computer");

            char choice;
            while (true)
            {
                choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (choice == '1')
                {
                    isAgainstComputer = false;
                    break;
                }
                else if (choice == '2')
                {
                    isAgainstComputer = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }
            }
        }

        private void PlayGame()
        {
            // Start a new game
            InitializeBoard();
            currentPlayer = 'X'; // 'X' starts first

            bool gameEnded = false;
            int moves = 0;

            while (!gameEnded && moves < 9)
            {
                Console.Clear(); // Clear the console before each move
                // Display the board
                DisplayBoard();

                // Get the current player's move
                MakeMove();

                // Check if the game has ended
                gameEnded = CheckForWin() || CheckForDraw();

                // Switch players
                currentPlayer = (currentPlayer == player1Symbol) ? player2Symbol : player1Symbol;

                moves++;
            }

            Console.Clear(); // Clear the console before displaying final results
            // Display the final board and result
            DisplayBoard();
            if (CheckForWin())
            {
                Console.WriteLine("Player " + (currentPlayer == player1Symbol ? player2Name : player1Name) + " wins!");
            }
            else if (CheckForDraw())
            {
                Console.WriteLine("It's a draw!");
            }
        }

        private bool AskToPlayAgain()
        {
            char choice;
            while (true)
            {
                Console.WriteLine("Do you want to play again? (Y/N)");
                choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (choice == 'Y')
                {
                    return true;
                }
                else if (choice == 'N')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter Y or N.");
                }
            }
        }

        private void DisplayBoard()
        {
            Console.WriteLine("  1 2 3");
            for (int row = 0; row < 3; row++)
            {
                Console.Write(row + 1 + " ");
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        private void MakeMove()
        {
            bool validMove = false;

            while (!validMove)
            {
                Console.WriteLine(currentPlayer == player1Symbol ? player1Name + "'s turn." : player2Name + "'s turn.");
                if (currentPlayer == player1Symbol || !isAgainstComputer)
                {
                    Console.Write("Enter your move (1-9): ");
                    char move = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    // Check if the chosen cell is empty
                    for (int row = 0; row < 3; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            if (board[row, col] == move)
                            {
                                board[row, col] = currentPlayer;
                                validMove = true;
                                return;
                            }
                        }
                    }
                    Console.WriteLine("Invalid move. Try again.");
                }
                else
                {
                    // Implement computer's move (random)
                    Random rnd = new Random();
                    int row, col;
                    do
                    {
                        row = rnd.Next(0, 3);
                        col = rnd.Next(0, 3);
                    } while (board[row, col] == 'X' || board[row, col] == 'O');

                    board[row, col] = currentPlayer;
                    validMove = true;
                    return;
                }
            }
        }

        private bool CheckForWin()
        {
            // Check rows, columns, and diagonals for a win
            return (CheckRows() || CheckColumns() || CheckDiagonals());
        }

        private bool CheckRows()
        {
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckColumns()
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDiagonals()
        {
            return ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) ||
                    (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]));
        }

        private bool CheckForDraw()
        {
            // If there are no empty cells and no winner, it's a draw
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        return false;
                    }
                }
            }
            return !CheckForWin();
        }
    }
}
