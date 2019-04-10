using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleGameWithMediator;

namespace ConsoleGameWithMediatorTests
{
    [TestClass]
    public class RulesHandlerShould
    {
        private string[,] testBoard;

        public RulesHandlerShould()
        {
            testBoard = BoardHandler.CreatePlayingBoard(9);
        }

        [TestMethod]
        public void BuildStringInDirectionRow()
        {
            testBoard[1, 1] = "X";
            testBoard[2, 1] = "X";
            testBoard[3, 1] = "X";
            testBoard[4, 1] = "O";
            testBoard[5, 1] = "X";

            RulesHandler rulesHandler = new RulesHandler(3);

            string testString = rulesHandler.BuildStringInDirection(testBoard, new int[]{ 1, 1}, 
                1, 0, "X");
            Assert.AreEqual("XX", testString);

        }

        [TestMethod]
        public void BuildStringInDirectionCol()
        {
            testBoard[1, 1] = "X";
            testBoard[1, 2] = "X";
            testBoard[1, 3] = "X";
            testBoard[1, 4] = "X";
            testBoard[1, 5] = "O";

            RulesHandler rulesHandler = new RulesHandler(3);

            string testString = rulesHandler.BuildStringInDirection(testBoard, new int[] { 1, 1 },
                0, 1, "X");
            Assert.AreEqual("XXX", testString);

        }

        [TestMethod]
        public void BuildStringInDirectionDiagonal()
        {
            testBoard[1, 1] = "X";
            testBoard[2, 2] = "X";
            testBoard[3, 3] = "X";
            testBoard[4, 4] = "X";
            testBoard[5, 5] = "O";

            RulesHandler rulesHandler = new RulesHandler(3);

            string testString = rulesHandler.BuildStringInDirection(testBoard, new int[] { 1, 1 },
                1, 1, "X");
            Assert.AreEqual("XXX", testString);

        }

    }
}
