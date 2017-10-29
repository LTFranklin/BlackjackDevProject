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
        static Deck deckInPlay = new Deck();

        static void Main(string[] args)
        {
            //constant loop to continously test games
            while (true)
            {
                Deal(playerHand);
                Deal(dealerHand);
                Deal(playerHand);
                Deal(dealerHand);
                play();
                playerHand.Clear();
                dealerHand.Clear();
            }
        }

        static bool Deal(Hand hand)
        {
            hand.AddCard(deckInPlay.GetCard());
            return hand.HandValueBool();
        }

        static void play()
        {
            bool winBool;
            bool continueBool = true;
            //While the player wishes to/ can continue
            while (continueBool)
            {
                //print out the hand - testing
                playerHand.PrintHand();
               
                continueBool = Play(playerHand);
            }
            //check if the player won
            winBool = WinCheck(playerHand);
            //print out the result - testing
            if (winBool)
            {
                Console.WriteLine("You win");
            }
            else
            {
                Console.WriteLine("You lose");
            }

        }

        //main player choice functions
        static bool Play(Hand hand)
        {
            //get the players decision
            bool choice = Decision(hand);
            //if they choose to stand
            if (!choice)
            {
                //do nothing and break out of the loop
                return false;
            }
            //if they choose to hit
            else
            {
                //use the deal method and put the result into the continue bool
                return Deal(hand);
            }
        }

        //checks the win condition
        static bool WinCheck(Hand hand)
        {
            //if the player hand was bust
            if (!hand.HandValueBool())
            {
                //print it out - testing
                hand.PrintHand();
                Console.WriteLine("Bust");
                return false;
            }
            //else compare the hand to the dealers
            else
            {
                //print out the hands - testing
                Console.WriteLine("Player:");
                hand.PrintHand();
                Console.WriteLine("Dealer");
                dealerHand.PrintHand();
                //if the player has a higher hand value return true
                if (hand.HandValueInt() > dealerHand.HandValueInt())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        static bool Decision(Hand hand)
        {
            //take input - testing until player AI is implemented
            if (Console.ReadLine() == "x")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
