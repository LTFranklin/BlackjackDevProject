using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    public class Program
    {
        //variables used
        static int pot = 0;
        static double bettingAmount = 100;
        static int intialBet = 2;
        static int wins = 0;
        static int losses = 0;
        static Deck deckInPlay;


        static void Main(string[] args)
        {
            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();
            Hand doublesHand = new Hand();

            for (int j = 0; j < 10; ++j)
            {
                deckInPlay = new Deck();
                //constant loop to continously test games
                for (int i = 0; i < 10; ++i)
                {
                    //deal the cards
                    DealKnown(playerHand);
                    DealKnown(dealerHand);
                    DealKnown(playerHand);
                    DealUnknown(dealerHand);
                    //take the bet
                    pot += intialBet;
                    Play(playerHand, doublesHand, dealerHand);
                    //empty the pot and clear the hands
                    pot = 0;
                    playerHand.Clear();
                    dealerHand.Clear();
                    doublesHand.Clear();
                }
                Console.WriteLine("Wins: {0}\tLosses: {1}\tPot: {2}", wins, losses, bettingAmount.ToString());
            }
        }

        //used for face up cards
        static bool DealKnown (Hand hand)
        {
            Card c = deckInPlay.GetCard();
            hand.AddCard(c);
            //changes the index value
            deckInPlay.EditIndexVal(c);
            bool b = hand.HandValueBool();
            return b;
        }

        //used for the dealers intial face down card
        static bool DealUnknown(Hand hand)
        {
            hand.AddCard(deckInPlay.GetCard());
            return hand.HandValueBool();
        }

        //some functions return values for testing
        static bool Play(Hand playerHand, Hand doublesHand, Hand dealerHand)
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

                    continueBool = Act(playerHand, doublesHand, dealerHand);
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
                    doublesHand = temp;
                    doubles = true;
                }
            }
            //check if the player won
            if (!doublesHand.IsEmpty())
            {
                WinCheck(doublesHand, dealerHand);
            }
            bool b = WinCheck(playerHand, dealerHand);
            return b;

        }

        //main player choice functions
        static bool Act(Hand playerHand, Hand doublesHand, Hand dealerHand)
        {
            //get the players decision
            string choice = Decision(playerHand, dealerHand);
            switch (choice)
            {
                //falg the player wishes to stop
                case ("stand"):
                    return false;
                //add a card to the players hand anf flag if its bust
                case ("hit"):
                    return DealKnown(playerHand);
                //split the hand
                case ("split"):
                    SplitHand(playerHand, doublesHand);
                    DealKnown(doublesHand);
                    return DealKnown(playerHand);
                //hit and increase the bet
                case ("double"):
                    pot += intialBet;
                    return DealKnown(playerHand);
                //split the hand and increase the bet
                case ("doubleSplit"):
                    pot += intialBet;
                    SplitHand(playerHand, doublesHand);
                    DealKnown(doublesHand);
                    return DealKnown(playerHand);
                default:
                    return false;
            }
        }
        
        //checks the win condition
        public static bool WinCheck(Hand hand, Hand dealerHand)
        {
            //if the player hand was bust and they lose their bet
            if (!hand.HandValueBool())
            {
                hand.PrintHand();
                Console.WriteLine("Bust, hand loses\n");
                losses++;
                bettingAmount -= pot;
                return false;
            }
            //else compare the hand to the dealers
            else
            {
                //go throught the dealers options
                DealerAction(dealerHand);
                //print out the hands - testing
                Console.WriteLine("Player:");
                hand.PrintHand();
                Console.WriteLine("Dealer");
                dealerHand.PrintHand();
                //if the player has a higher hand value then they win
                if (hand.HandValueInt() > dealerHand.HandValueInt())
                {
                    //if they have blackjack
                    if (hand.BlackjackCheck())
                    {
                        Console.WriteLine("Blackjack, Hand Wins\n");
                        //give the 3/2 payout
                        bettingAmount += (pot * 1.5);
                        wins++;
                        return true;
                    }
                    Console.WriteLine("Hand wins\n");
                    bettingAmount += pot;
                    wins++;
                    return true;
                }
                //if its a draw the bet is returned
                if(hand.HandValueInt() == dealerHand.HandValueInt())
                {
                    Console.WriteLine("Draw, bet returned\n");
                    return false;
                }
                //if the dealer wins the player gets nothing back
                if (dealerHand.HandValueBool())
                {
                    Console.WriteLine("Hand loses\n");
                    losses++;
                    bettingAmount -= pot;
                    return false;
                }
                //if the dealer is bust and the player isnt they win
                else
                {
                    Console.WriteLine("Dealer bust, hand wins\n");
                    wins++;
                    //give the player their intial bet back + the amount they get for winning
                    bettingAmount += pot;
                    return true;
                }
            }
        }
        
        static string Decision(Hand hand, Hand dealerHand)
        {
            Player p = new Player(deckInPlay.GetIndexValue());
            return p.basicStrat(hand.GetCard(0).GetVal(),hand.GetCard(1).GetVal(),hand.HandValueInt(), dealerHand.GetCard(0).GetVal());

        }

        static void SplitHand(Hand playerHand, Hand doublesHand)
        {
            doublesHand.AddCard(playerHand.GetCard(1));
            playerHand.RemoveCard(1);
        }

        static void DealerAction(Hand dealerHand)
        {
            while(dealerHand.HandValueInt() < 17)
            {
                DealKnown(dealerHand);
            }
            return;
        }
    }
}
