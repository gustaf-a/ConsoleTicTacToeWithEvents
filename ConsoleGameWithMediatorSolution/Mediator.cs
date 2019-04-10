using System;

namespace ConsoleGameWithMediator
{
    public sealed class Mediator
    {
        //Singleton
        private static readonly Mediator _thisMediator = new Mediator();
        private Mediator() {
        }

        public static Mediator GetMediator()
        {
        return _thisMediator;
        }

        //Mediator Functions
        public RulesHandler RulesHandler { get; set; }
        public BoardHandler BoardHandler { get; set; }

        public event EventHandler<NewMoveAttemptEventArgs> NewMoveAttempted;
        public event EventHandler<NewMoveEventArgs> NewMoveMade;
        public event EventHandler<NewMoveEventArgs> PlayerHasWon;

        //Triggered from Player
        public bool OnNewMoveAttempt(Object sender, int[] newMoveCoordinates, string playerSymbol)
        {
            var NewMoveAttemptedDelegate = NewMoveAttempted as EventHandler<NewMoveAttemptEventArgs>;
            if (NewMoveAttemptedDelegate != null)
            {
                NewMoveAttemptEventArgs thisMoveAttemptEventArgs =
                    new NewMoveAttemptEventArgs { Coordinates = newMoveCoordinates, theBoard = BoardHandler.GetBoard() };
                NewMoveAttemptedDelegate(sender, thisMoveAttemptEventArgs);
                return thisMoveAttemptEventArgs.MoveAllowed;
            }
            Console.WriteLine("Error when raising OnNewMoveAttemptEvent");
            return false;
        }

        //Triggered from Player
        public void OnNewMoveMade(Object sender, int[] newMoveCoordinates, string playerSymbol)
        {
            var NewMoveMadeDelegate = NewMoveMade as EventHandler<NewMoveEventArgs>;
            if (NewMoveMadeDelegate != null)
            {
                NewMoveEventArgs thisNewMoveEventArgs =
                    new NewMoveEventArgs { Coordinates = newMoveCoordinates, PlayerSymbol = playerSymbol,
                        theBoard = BoardHandler.GetBoard() };
                NewMoveMadeDelegate(sender, thisNewMoveEventArgs);
                if (thisNewMoveEventArgs.PlayerHasWon)
                {
                    OnPlayerHasWon(sender, playerSymbol);
                }
            }
        }

        //Triggered from RulesHandler
        public void OnPlayerHasWon(Object sender, string playerSymbol)
        {
            var playerWonDelegate = PlayerHasWon as EventHandler<NewMoveEventArgs>;
            if (playerWonDelegate != null)
            {
                playerWonDelegate(sender, new NewMoveEventArgs { PlayerSymbol = playerSymbol });
            }
        }
    }

    public class NewMoveAttemptEventArgs
    {
        public int[] Coordinates { get; set; }
        public string[,] theBoard;
        public bool MoveAllowed { get; set; }
        public string ErrorMessage{ get; set; }
    }

    public class NewMoveEventArgs
    {
        public int[] Coordinates { get; set; }
        public string[,] theBoard;
        public string PlayerSymbol { get; set; }
        public bool PlayerHasWon { get; set; }
        public string ErrorMessage { get; set; }
    }
}
