using System;

namespace ConsoleGameWithMediator
{
    public class RulesHandler
    {
        private int _winCondition;

        public RulesHandler(int winCondition)
        {
            Mediator.GetMediator().NewMoveAttempted += (s, e) => IsMoveAttemptValid(s, e);
            Mediator.GetMediator().NewMoveMade += (s, e) => DidPlayerWin(s, e);
            _winCondition = winCondition;
        }

        private void IsMoveAttemptValid(object s, NewMoveAttemptEventArgs e)
        {
            string currentValueInCell = e.theBoard[e.Coordinates[0], e.Coordinates[1]];
            if (String.IsNullOrWhiteSpace(currentValueInCell))
            {
                e.MoveAllowed = true;
            }
            else
            {
                e.MoveAllowed = false;
                Console.WriteLine("Cell already played in. Please try again");
            }
        }

        private void DidPlayerWin(object s, NewMoveEventArgs e)
        {
            if (CheckVictory(e, 1, 0)
                || CheckVictory(e, 0, 1)
                || CheckVictory(e, 1, 1)
                || CheckVictory(e, 1, -1)
                )
            {
                e.PlayerHasWon = true;
                return;
            }
            e.PlayerHasWon = false;
        }

        public bool CheckVictory(NewMoveEventArgs e, int rowIncrement, int colIncrement)
        {
            string currentString = BuildStringInDirection(e.theBoard, e.Coordinates, rowIncrement, colIncrement, e.PlayerSymbol)
                + e.PlayerSymbol
                + BuildStringInDirection(e.theBoard, e.Coordinates, -rowIncrement, -colIncrement, e.PlayerSymbol);
            if (currentString.Length >= _winCondition)
            {
                return true;
            }
            return false;
        }

        //Skips starting point value and returns the next in direction specified by increments
        public string BuildStringInDirection(string[,] theBoard, int[] coordinates,
            int rowIncrement, int colIncrement, string playerSymbol)
        {
            int newRow = coordinates[0] + rowIncrement;
            int newCol = coordinates[1] + colIncrement;
            if (newRow < 0
                || newCol < 0
                || newRow > (theBoard.GetLength(0) - 1)
                || newCol > (theBoard.GetLength(1) - 1))
            {
                return "";
            }
            int[] newCoordinates = new int[] { newRow, newCol };
            string nextValue = theBoard[newCoordinates[0], newCoordinates[1]];
            if (nextValue == playerSymbol)
            {
                return playerSymbol + BuildStringInDirection(theBoard, newCoordinates,
                    rowIncrement, colIncrement, playerSymbol);
            }
            return "";
        }
    }
}
