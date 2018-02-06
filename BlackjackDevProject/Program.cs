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

        static Hand playerHand = new Hand();
        static Hand dealerHand = new Hand();
        static Hand doublesHand = new Hand();
        static List<Card> unknown = new Deck().GetDeck();
        static Deck deckInPlay;
        static int pot = 0;
        static double bettingAmount = 100;
        static int intialBet = 2;
        static int wins = 0;
        static int losses = 0;
        static int a = 0;
        static int b = 0;

        static void Main(string[] args)
        {
            for (int j = 0; j < 10; ++j)
            {
                deckInPlay = new Deck();
                //constant loop to continously test games
                for (int i = 0; i < 10; ++i)
                {

                    //deal the cards
                    Deal(playerHand);
                    Deal(dealerHand);
                    Deal(playerHand);
                    Deal(dealerHand);
                    //take the bet
                    pot += intialBet;
                    Play();
                    //empty the pot and clear the hands
                    pot = 0;
                    playerHand.Clear();
                    dealerHand.Clear();
                    doublesHand.Clear();
                }
                Console.WriteLine("Wins: {0}\tLosses: {1}\tPot: {2}", wins, losses, bettingAmount.ToString());
            }
        }

        static bool Deal(Hand hand)
        {
            hand.AddCard(deckInPlay.GetCard()); 
            return hand.HandValueBool();
        }

        static void Play()
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

                    continueBool = Act(playerHand);
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
            if(!doublesHand.IsEmpty())
            {
                ++b;
                WinCheck(doublesHand);
            }
            WinCheck(playerHand);

        }

        //main player choice functions
        static bool Act(Hand hand)
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
                    SplitHand();
                    Deal(doublesHand);
                    return Deal(hand);
                //hit and increase the bet
                case ("double"):
                    pot += intialBet;
                    return Deal(hand);
                //split the hand and increase the bet
                case ("doubleSplit"):
                    pot += intialBet;
                    SplitHand();
                    Deal(doublesHand);
                    return Deal(hand);
                default:
                    return false;
            }
        }
        
        //checks the win condition
        static void WinCheck(Hand hand)
        {
            //if the player hand was bust and they lose their bet
            if (!hand.HandValueBool())
            {
                hand.PrintHand();
                Console.WriteLine("Bust, hand loses\n");
                losses++;
                bettingAmount -= pot;
                return;
            }
            //else compare the hand to the dealers
            else
            {
                //go throught the dealers options
                DealerAction();
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
                        return;
                    }
                    Console.WriteLine("Hand wins\n");
                    bettingAmount += pot;
                    wins++;
                    return;
                }
                else
                {
                    //if the dealer wins the player gets nothing back
                    if (dealerHand.HandValueBool())
                    {
                        Console.WriteLine("Hand loses\n");
                        losses++;
                        bettingAmount -= pot;
                        return;
                    }
                    //if the dealer is bust and the player isnt they win
                    else
                    {
                        Console.WriteLine("Dealer bust, hand wins\n");
                        wins++;
                        //give the player their intial bet back + the amount they get for winning
                        bettingAmount += pot;
                        return;
                    }
                }
            }
        }
        
        static string Decision(Hand hand)
        {
            Player p = new Player();
            return p.basicStrat(hand.GetCard(0).GetVal(),hand.GetCard(1).GetVal(),hand.HandValueInt(), dealerHand.GetCard(0).GetVal());

        }

        static void SplitHand()
        {
            doublesHand.AddCard(playerHand.GetCard(1));
            playerHand.RemoveCard(1);
            ++a;
        }

        static void DealerAction()
        {
            while(dealerHand.HandValueInt() < 17)
            {
                Deal(dealerHand);
            }
        }
    }
}
