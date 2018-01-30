using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    class Program
    {
        //variables used

        static Hand playerHand = new Hand();
        static Hand dealerHand = new Hand();
        static Hand doublesHand = new Hand();
        static List<Card> unknown = new Deck().GetDeck();
        static Deck deckInPlay;
        static int wins = 0;
        static int losses = 0;

        static void Main(string[] args)
        {
            for (int j = 0; j < 25; ++j)
            {
                deckInPlay = new Deck();
                //constant loop to continously test games
                for (int i = 0; i < 25; ++i)
                {
                    Deal(playerHand);
                    Deal(dealerHand);
                    Deal(playerHand);
                    Deal(dealerHand);
                    play();
                    playerHand.Clear();
                    dealerHand.Clear();
                    doublesHand.Clear();
                }
                Console.WriteLine("Wins: {0}\tLosses: {1}", wins, losses);
            }
        }

        static bool Deal(Hand hand)
        {
            hand.AddCard(deckInPlay.GetCard()); 
            return hand.HandValueBool();
        }

        static void play()
        {
            bool doubles = false;
            bool inPlay = true;
            //While the player wishes to/ can continue
            while (inPlay)
            {
                bool continueBool = true;
                while (continueBool)
                {
                    //print out the hand - testing
                    playerHand.PrintHand();

                    continueBool = act(playerHand);
                }
                //check if a hand was split
                if(doublesHand.IsEmpty() || doubles)
                {
                    inPlay = false;
                }
                else
                {
                    Hand temp = playerHand;
                    playerHand = doublesHand;
                    doublesHand = playerHand;
                    doubles = true;
                }
            }
            //check if the player won
            if(!doublesHand.IsEmpty())
            {
                WinCheck(doublesHand);
            }
            WinCheck(playerHand);

        }

        //main player choice functions
        static bool act(Hand hand)
        {
            //get the players decision
            string choice = Decision(hand);
            switch (choice)
            {
                //falg the player wishes to stop
                case ("stand"):
                    return false;
                //add a card to the players hand anf flag if its bust
                case ("hit"):
                    return Deal(hand);
                //split the hand
                case ("split"):
                    splitHand();
                    Deal(doublesHand);
                    return Deal(hand);
                //hit and increase the bet
                case ("double"):
                    //implement betting
                    return Deal(hand);
                //split the hand and increase the bet
                case ("doubleSplit"):
                    //implement betting
                    splitHand();
                    Deal(doublesHand);
                    return Deal(hand);
                default:
                    return false;
            }
        }
        
        //checks the win condition
        static void WinCheck(Hand hand)
        {
            //if the player hand was bust
            if (!hand.HandValueBool())
            {
                //print it out - testing
                hand.PrintHand();
                Console.WriteLine("Bust, hand loses\n");
                losses++;
            }
            //else compare the hand to the dealers
            else
            {
                //go throught the dealers options
                dealerAction();
                //print out the hands - testing
                Console.WriteLine("Player:");
                hand.PrintHand();
                Console.WriteLine("Dealer");
                dealerHand.PrintHand();
                //if the player has a higher hand value return true
                if (hand.HandValueInt() > dealerHand.HandValueInt())
                {
                    Console.WriteLine("Hand wins\n");
                    wins++;
                }
                else
                {
                    if (dealerHand.HandValueBool())
                    {
                        Console.WriteLine("Hand loses\n");
                        losses++;
                    }
                    else
                    {
                        Console.WriteLine("Dealer bust, hand wins\n");
                        wins++;
                    }
                }
            }
        }
        
        static string Decision(Hand hand)
        {
            Player p = new Player();
            return p.basicStrat(hand.GetCard(0).GetVal(),hand.GetCard(1).GetVal(),hand.HandValueInt(), dealerHand.GetCard(0).GetVal());

        }

        static void splitHand()
        {
            doublesHand.AddCard(playerHand.GetCard(1));
            playerHand.RemoveCard(1);
        }

        static void dealerAction()
        {
            while(dealerHand.HandValueInt() < 17)
            {
                Deal(dealerHand);
            }
        }
    }
}
