using System;
using System.Collections.Generic;

namespace ConsoleGameWithMediator
{
    //public delegate void MoveMadeHandler(object sender, MoveMadeEventArgs e);
    public delegate bool NewMoveDelegate(string[,] gameField, int[] newMove);

    class Program
    {

        private static List<Player> players;
        private static bool gameIsWon;
        private static string[] playerSymbols = new string[] { "X", "O", "W", "T", "B" };
        private static string[] playersRecBoardSize = new string[] { "0", "3", "3-6", "6-9", "7-10", "9-15", "12-20" };
        private static int boardSize;
        private static int winCondition;
        private static int numberPlayers;
        private static int gamesPlayed;


        private delegate bool InputValidationDelegate(string input);
        private delegate void InputWhenValidDelegate(string input);

        static void Main(string[] args)
        {
            //Basic settings
            boardSize = 9;
            winCondition = 3;
            numberPlayers=2;
            gamesPlayed = 0;

            Console.WriteLine("Welcome to this simple something-in-a-row game.");

            GetUserInput();

            players = new List<Player>();
            for (int i = 0; i < numberPlayers; i++)
            {
                players.Add(new PlayerHuman(playerSymbols[i]));
            }

            //Big while for many games
            do
            {

                gameIsWon = false;

                //Init Mediator
                Mediator.GetMediator().BoardHandler = new BoardHandler(boardSize);
                Mediator.GetMediator().RulesHandler = new RulesHandler(winCondition);

                if (gamesPlayed==0)
                {
                    //Bind to PlayerHasWon
                    Mediator.GetMediator().PlayerHasWon += (s, e) => gameIsWon = true;
                }
                //Init board
                Mediator.GetMediator().BoardHandler.DrawGUI();

                //Small while for 1 game
                do
                {
                    foreach (Player player in players)
                    {
                        player.DoMove();
                        if (gameIsWon)
                        {
                            break;
                        }
                    }
                } while (!gameIsWon);
                gamesPlayed++;
                Console.WriteLine("Thank you for playing. To play again press enter. To exit, press any other key");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        private static void GetUserInput()
        {
            //NUMBER PLAYERS
            InputValidationDelegate numberPlayersValidateDelegate =
                (input) => {
                    int inputInt = Int32.Parse(input);
                    if (1 < inputInt && inputInt < 6) return true;
                    return false;
                };
            InputWhenValidDelegate whenPlayersValidated = (input) => numberPlayers = Int32.Parse(input);

            //Send delegate into function that asks for input
            GetInput("Please insert the number of players [2-5]",
                numberPlayersValidateDelegate, whenPlayersValidated);

            //WIN CONDITION
            InputValidationDelegate winConditionValidationDelegate =
                (input) => {
                    int inputInt = Int32.Parse(input);
                    if (1 < inputInt && inputInt < boardSize) return true;
                    return false;
                };
            InputWhenValidDelegate whenWinConditionValidated = (input) => winCondition = Int32.Parse(input);

            //Send delegate into function that asks for input
            GetInput($"Please chose win condition. Recommended is 3, 4 or 5",
                winConditionValidationDelegate, whenWinConditionValidated);

            //BOARD SIZE
            InputValidationDelegate boardSizeValidationDelegate =
                (input) => {
                    int inputInt = Int32.Parse(input);
                    if (2 < inputInt) return true;
                    return false;
                };
            InputWhenValidDelegate whenBoardSizeValidated = (input) => boardSize = Int32.Parse(input);

            //Send delegate into function that asks for input
            GetInput($"Please chose boardsize. The recommended board size for {numberPlayers.ToString()} players is {playersRecBoardSize[numberPlayers]}",
                boardSizeValidationDelegate, whenBoardSizeValidated);

        }

        private static void GetInput(string message, InputValidationDelegate inputStringValidationDelegate, InputWhenValidDelegate doWhenValidatedDelegate)
        {
            bool inputValid = false;
            do
            {
                Console.WriteLine(message);
                try
                {
                    string inputString = Console.ReadLine();
                    if (inputStringValidationDelegate(inputString))
                    {
                        doWhenValidatedDelegate(inputString);
                        inputValid = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            } while (!inputValid);
        }
    }
}
