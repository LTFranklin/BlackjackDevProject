using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackDevProject;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        //might be best changing methods in player to return numbers os split hands can be tested along with other player choices?

        public Hand BuildHand()
        {
            //set up the dealers hand -> constant throughout
            Hand dealerHand = new Hand();
            Card c = new Card(10, 0);
            dealerHand.AddCard(c);
            c = new Card(7, 0);
            dealerHand.AddCard(c);
            return dealerHand;
        }

        [TestMethod]
        public void StandardWins()
        {
            Hand dealerHand = BuildHand();
            GameFeatures state = new GameFeatures();

            //test a standard winnning hand
            Hand playerHand = new Hand();
            Card c = new Card(10, 1);
            playerHand.AddCard(c);
            c = new Card(8, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a picture winning hand
            c = new Card(11, 0);
            playerHand.AddCard(c);
            c = new Card(8, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a high card count winning hand
            c = new Card(2, 0);
            playerHand.AddCard(c);
            c = new Card(2, 1);
            playerHand.AddCard(c);
            c = new Card(2, 2);
            playerHand.AddCard(c);
            c = new Card(2, 3);
            playerHand.AddCard(c);
            c = new Card(3, 0);
            playerHand.AddCard(c);
            c = new Card(3, 1);
            playerHand.AddCard(c);
            c = new Card(3, 2);
            playerHand.AddCard(c);
            c = new Card(3, 3);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();
        }

        [TestMethod]
        public void StandardLosses()
        {
            Hand dealerHand = BuildHand();
            GameFeatures state = new GameFeatures();

            //test a standard losing hand
            Hand playerHand = new Hand();
            Card c = new Card(7, 1);
            playerHand.AddCard(c);
            c = new Card(3, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(false, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a picture losing hand
            c = new Card(11, 0);
            playerHand.AddCard(c);
            c = new Card(5, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(false, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a high card count losing hand
            c = new Card(2, 0);
            playerHand.AddCard(c);
            c = new Card(2, 1);
            playerHand.AddCard(c);
            c = new Card(2, 2);
            playerHand.AddCard(c);
            c = new Card(3, 1);
            playerHand.AddCard(c);
            c = new Card(3, 2);
            playerHand.AddCard(c);
            c = new Card(3, 3);
            playerHand.AddCard(c);
            Assert.AreEqual(false, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();
        }

        [TestMethod]
        public void AceHands()
        {
            Hand dealerHand = BuildHand();
            GameFeatures state = new GameFeatures();

            //test a winnning hand
            Hand playerHand = new Hand();
            Card c = new Card(1, 1);
            playerHand.AddCard(c);
            c = new Card(8, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a picture winning hand
            c = new Card(11, 0);
            playerHand.AddCard(c);
            c = new Card(1, 0);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();

            //test a high card count winning hand -> fails
            c = new Card(1, 0);
            playerHand.AddCard(c);
            c = new Card(1, 1);
            playerHand.AddCard(c);
            c = new Card(1, 2);
            playerHand.AddCard(c);
            c = new Card(1, 3);
            playerHand.AddCard(c);
            c = new Card(2, 0);
            playerHand.AddCard(c);
            c = new Card(2, 1);
            playerHand.AddCard(c);
            c = new Card(2, 2);
            playerHand.AddCard(c);
            c = new Card(2, 3);
            playerHand.AddCard(c);
            c = new Card(3, 0);
            playerHand.AddCard(c);
            c = new Card(3, 1);
            playerHand.AddCard(c);
            c = new Card(3, 2);
            playerHand.AddCard(c);
            Assert.AreEqual(true, Program.WinCheck(playerHand, dealerHand, state));
            playerHand.Clear();
        }

    }
}
