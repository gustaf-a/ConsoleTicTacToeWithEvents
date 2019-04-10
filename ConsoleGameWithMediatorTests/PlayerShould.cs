using ConsoleGameWithMediator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleGameWithMediatorTests
{
    [TestClass]
    public class PlayerShould
    {
        [TestMethod]
        public void HavePlayerSymbolWhenNew()
        {
            string symbol = "X";
            Player player = new PlayerHuman(symbol);
            Assert.AreEqual(player.GetPlayerSymbol(false), symbol);
        }
        [TestMethod]
        public void ReturnPlayerSymbolWithPadding()
        {
            string symbol = "X";
            int length = symbol.Length;
            Player player = new PlayerHuman(symbol);
            Assert.IsTrue(player.GetPlayerSymbol().Length>length);
        }
    }
}
