using System;

namespace ConsoleGameWithMediator
{
    public abstract class Player
    {
        public abstract void DoMove();
        protected abstract int[] NewMove();
        protected string _playerSymbol;

        protected Player(string playerSymbol)
        {
            _playerSymbol = playerSymbol;
        }

        public string GetPlayerSymbol(bool withPadding = true)
        {
            string padding = "";
            if (withPadding)
            {
                padding = " ";
            }
            return padding + _playerSymbol + padding;
        }
    }

    public class PlayerHuman : Player
    {
        public PlayerHuman(string playerSymbol) : base(playerSymbol)
        {
        }

        public override void DoMove()
        {
            bool MoveAccepted = false;
            int[] newMove = new int[] { };
            do
            {
                try
                {
                    newMove = NewMove();
                    MoveAccepted = Mediator.GetMediator().OnNewMoveAttempt(this, newMove, _playerSymbol);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please insert coordinates using numbers separated by a comma (,)");
                }

            } while (!MoveAccepted);
            Mediator.GetMediator().OnNewMoveMade(this, newMove, _playerSymbol);
        }

        protected override int[] NewMove()
        {
            Console.WriteLine($"Please enter coordinates (for example 3,4) for your move Player {_playerSymbol} ");
            string[] inputLines=Console.ReadLine().ToString().Split(',');
            int x = Int32.Parse(inputLines[0].Trim());
            int y = Int32.Parse(inputLines[1].Trim());
            return new int[] { x, y };
        }

    }

    public class RandomAI : Player
    {
        public RandomAI(string playerSymbol) : base(playerSymbol)
        {
        }

        public override void DoMove()
        {
            bool MoveAccepted = false;
            int[] newMove = new int[] { };
            do
            {
                try
                {
                    newMove = NewMove();
                    MoveAccepted = Mediator.GetMediator().OnNewMoveAttempt(this, newMove, _playerSymbol);
                }
                catch (Exception)
                {
                    Console.Write("Bad move from AI");
                }

            } while (!MoveAccepted);
            Mediator.GetMediator().OnNewMoveMade(this, newMove, _playerSymbol);
        }

        protected override int[] NewMove()
        {
            //TODO THE AI MUST HAVE ACCESS TO BOARD
            Random rnd = new Random();
            int x = rnd.Next(0, 9);
            int y = rnd.Next(0, 9);
            return new int[] { x, y };
        }
    }
}
