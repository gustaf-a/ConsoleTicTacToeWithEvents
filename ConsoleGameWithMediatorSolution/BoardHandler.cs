using System;
using System.Text;

namespace ConsoleGameWithMediator
{
    public class BoardHandler
    {
        private string[,] _theBoard;
        private readonly int _boardSize;
        private string gameHeader;
        private string gameFooter;

        public int GameFieldStartString { get; private set; }

        public BoardHandler(int boardSize)
        {
            _boardSize = boardSize;
            _theBoard = CreatePlayingBoard(_boardSize);
            gameHeader = CreateHeader(boardSize);
            gameFooter = CreateFooter(boardSize);


            //Bind to NewMoveAttempted
            Mediator.GetMediator().NewMoveAttempted += (s, e) => DrawGUI(e.ErrorMessage);

            //Bind to NewMoveMade
            Mediator.GetMediator().NewMoveMade += (s, e) => UpdateBoard(e.Coordinates, e.PlayerSymbol);
            Mediator.GetMediator().NewMoveMade += (s, e) => DrawGUI();
            Mediator.GetMediator().PlayerHasWon += (s, e) => PlayerHasWon(s, e);
        }

        private string CreateHeader(int boardSize)
        {
            return " ";
        }

        private string CreateFooter(int boardSize)
        {
            return " ";
        }


        public string[,] GetBoard()
        {
            return _theBoard;
        }

        public static string[,] CreatePlayingBoard(int boardSize)
        {
            string[,] board = new string[boardSize, boardSize];
            return board;
        }

        public void DrawGUI(string message = "", bool showCoordinates = true)
        {
            Console.Clear();
            PrintHeader();
            PrintBoard(showCoordinates);
            PrintFooter();
            Console.WriteLine(message);
        }

        private void PrintHeader()
        {
            Console.WriteLine(gameHeader);

        }

        private void PrintFooter()
        {
            Console.WriteLine(gameFooter);
        }

        public void UpdateBoard(int[] coordinates, string playerSymbol)
        {
            _theBoard[coordinates[0], coordinates[1]] = playerSymbol;
        }

        public string[,] GetTheBoard()
        {
            return _theBoard;
        }

        private void PrintBoard(bool showCoordinates)
        {
            StringBuilder sb = new StringBuilder(GameFieldStartString, StringBuilderSize(_boardSize));
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    sb.Append("  ");
                    if (String.IsNullOrWhiteSpace(_theBoard[i, j]))
                    {
                        if (showCoordinates)
                        {
                            sb.Append(i.ToString() + "," + j.ToString());
                        }
                        else
                        {
                            sb.Append("   ");
                        }
                    }
                    else
                    {
                        sb.Append(" " + _theBoard[i, j] + " ");
                    }
                    sb.Append("  ");
                }
                sb.AppendLine();
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        private int StringBuilderSize(int length)
        {
            int size = (6 * length + 2) * length * length;
            return size;
        }

        private void PlayerHasWon(object s, NewMoveEventArgs e)
        {
            DrawGUI($"Congratulations! Player {e.PlayerSymbol} has won!", false);
        }

    }
}
