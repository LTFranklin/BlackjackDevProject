using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    public class Program
    {
        //variables used for win tracking
        static int wins = 0;
        static int losses = 0;


        static void Main(string[] args)
        {
            GameFeatures gameState = new GameFeatures();
            Hand playerHand = new Hand();
            Hand dealerHand = new Hand();
            Hand doublesHand = new Hand();

            for (int j = 0; j < 10; ++j)
            {
                gameState.SetDeck(new Deck());
                //constant loop to continously test games
                for (int i = 0; i < 10; ++i)
                {
                    //deal the cards
                    DealKnown(playerHand, gameState);
                    DealKnown(dealerHand, gameState);
                    DealKnown(playerHand, gameState);
                    DealUnknown(dealerHand, gameState);
                    //take the bet
                    gameState.IncrementPot();
                    Play(playerHand, doublesHand, dealerHand, gameState);
                    //empty the pot and clear the hands
                    gameState.SetPot(0);
                    playerHand.Clear();
                    dealerHand.Clear();
                    doublesHand.Clear();
                }
                Console.WriteLine("Wins: {0}\tLosses: {1}\tPot: {2}", wins, losses, gameState.GetBettingAmount().ToString());
            }
            Console.ReadLine();
        }

        //used for face up cards
        static bool DealKnown (Hand hand, GameFeatures gameState)
        {
            Card c = gameState.GetDeck().GetCard();
            hand.AddCard(c);
            //changes the index value
            gameState.GetDeck().EditIndexVal(c);
            bool b = hand.HandValueBool();
            return b;
        }

        //used for the dealers intial face down card
        static bool DealUnknown(Hand hand, GameFeatures gameState)
        {
            hand.AddCard(gameState.GetDeck().GetCard());
            return hand.HandValueBool();
        }

        //some functions return values for testing
        static bool Play(Hand playerHand, Hand doublesHand, Hand dealerHand, GameFeatures gameState)
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

                    continueBool = Act(playerHand, doublesHand, dealerHand, gameState);
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
                WinCheck(doublesHand, dealerHand, gameState);
            }
            bool b = WinCheck(playerHand, dealerHand, gameState);
            return b;

        }

        //main player choice functions
        static bool Act(Hand playerHand, Hand doublesHand, Hand dealerHand, GameFeatures gameState)
        {
            //get the players decision
            string choice = Decision(playerHand, dealerHand, gameState);
            switch (choice)
            {
                //falg the player wishes to stop
                case ("stand"):
                    return false;
                //add a card to the players hand anf flag if its bust
                case ("hit"):
                    return DealKnown(playerHand, gameState);
                //split the hand
                case ("split"):
                    SplitHand(playerHand, doublesHand);
                    DealKnown(doublesHand, gameState);
                    return DealKnown(playerHand, gameState);
                //hit and increase the bet
                case ("double"):
                    gameState.IncrementPot();
                    return DealKnown(playerHand, gameState);
                //split the hand and increase the bet
                case ("doubleSplit"):
                    gameState.IncrementPot();
                    SplitHand(playerHand, doublesHand);
                    DealKnown(doublesHand, gameState);
                    return DealKnown(playerHand, gameState);
                default:
                    return false;
            }
        }
        
        //checks the win condition
        public static bool WinCheck(Hand hand, Hand dealerHand, GameFeatures gameState)
        {
            //if the player hand was bust and they lose their bet
            if (!hand.HandValueBool())
            {
                hand.PrintHand();
                Console.WriteLine("Bust, hand loses\n");
                losses++;
                gameState.ChangeBettingAmount(false);
                return false;
            }
            //else compare the hand to the dealers
            else
            {
                //go throught the dealers options
                DealerAction(dealerHand, gameState);
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
                        gameState.SetPot(gameState.GetPot() + 1.5);
                        wins++;
                        return true;
                    }
                    Console.WriteLine("Hand wins\n");
                    gameState.ChangeBettingAmount(true);
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
                    gameState.ChangeBettingAmount(false);
                    return false;
                }
                //if the dealer is bust and the player isnt they win
                else
                {
                    Console.WriteLine("Dealer bust, hand wins\n");
                    wins++;
                    //give the player their intial bet back + the amount they get for winning
                    gameState.ChangeBettingAmount(true);
                    return true;
                }
            }
        }
        
        static string Decision(Hand hand, Hand dealerHand, GameFeatures gameState)
        {
            Player p = new Player(gameState.GetDeck().GetIndexValue());
            return p.BasicStrat(hand.GetCard(0).GetVal(),hand.GetCard(1).GetVal(),hand.HandValueInt(), dealerHand.GetCard(0).GetVal());

        }

        static void SplitHand(Hand playerHand, Hand doublesHand)
        {
            doublesHand.AddCard(playerHand.GetCard(1));
            playerHand.RemoveCard(1);
        }

        static void DealerAction(Hand dealerHand, GameFeatures gameState)
        {
            while(dealerHand.HandValueInt() < 17)
            {
                DealKnown(dealerHand, gameState);
            }
            return;
        }
    }
}
