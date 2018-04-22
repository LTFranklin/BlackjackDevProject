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
        static double accuracy = 0;
        static double decisions = 0;
        static int hands = 0;
        static bool userInput = false;


        static void Main(string[] args)
        {
            GameFeatures gameState = new GameFeatures();

            for (int j = 0; j < 10000; ++j)
            {
                gameState.SetDeck(new Deck());
                //constant loop to continously test games
                for (int i = 0; i < 10; ++i)
                {
                    //deal the cards
                    DealKnown(gameState.GetHand("player"), gameState);
                    DealKnown(gameState.GetHand("dealer"), gameState);
                    DealKnown(gameState.GetHand("player"), gameState);
                    DealUnknown(gameState.GetHand("dealer"), gameState);
                    //take the bet
                    gameState.IncrementPot();
                    Play(gameState);
                    //empty the pot and clear the hands
                    gameState.SetPot(0);
                    gameState.GetHand("player").Clear();
                    gameState.GetHand("dealer").Clear();
                    gameState.GetHand("doubles").Clear();
                    ++hands;
                }
                double d = accuracy / decisions;
                Console.WriteLine("Wins: {0}\tLosses: {1}\tPot: {2}\tAccuracy: {3}", wins, losses, gameState.GetBettingAmount().ToString(), d.ToString());
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
            if(!b)
            {
                Console.WriteLine("Hand Bust\n");
            }
            return b;
        }

        //used for the dealers intial face down card
        static bool DealUnknown(Hand hand, GameFeatures gameState)
        {
            hand.AddCard(gameState.GetDeck().GetCard());
            return hand.HandValueBool();
        }

        //some functions return values for testing
        static bool Play(GameFeatures gameState)
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

                    ;
                    Console.Write("Player: ");
                    gameState.GetHand("player").PrintHand();
                    Console.WriteLine("Dealer: " + gameState.GetHand("dealer").GetCard(0).ToString() + "\n");
                    continueBool = Act(gameState);
                }
                //check if a hand was split
                if(gameState.GetHand("doubles").IsEmpty() || doubles)
                {
                    inPlay = false;
                }
                else
                {
                    Hand temp = gameState.GetHand("player");
                    gameState.SetHand("player", gameState.GetHand("doubles"));
                    gameState.SetHand("doubles", temp);
                    doubles = true;
                }
            }
            //check if the player won
            if (!gameState.GetHand("doubles").IsEmpty())
            {
                WinCheck(gameState.GetHand("doubles"), gameState.GetHand("dealer"), gameState);
            }
            bool b = WinCheck(gameState.GetHand("player"), gameState.GetHand("dealer"), gameState);
            return b;

        }

        //main player choice functions
        static bool Act(GameFeatures gameState)
        {
            ++decisions;
            //get the players decision
            string choice;
            //bool changes the gmae from manual to auto input
            while (true)
            {
                if (userInput)
                {
                    Console.WriteLine("What would you like to do?\n");
                    choice = Console.ReadLine();
                    //checks if the user input matches the AIs
                    if (Compare(gameState.GetHand("player"), gameState.GetHand("dealer"), gameState, choice))
                    {
                        ++accuracy;
                    }
                }
                else
                {
                    choice = Decision(gameState.GetHand("player"), gameState.GetHand("dealer"), gameState);
                }
                switch (choice)
                {
                    //falg the player wishes to stop
                    case ("stand"):
                        return false;
                    //add a card to the players hand anf flag if its bust
                    case ("hit"):
                        return DealKnown(gameState.GetHand("player"), gameState);
                    //split the hand
                    case ("split"):
                        if (gameState.GetHand("player").GetCardCount() > 2 || (gameState.GetHand("player").GetCard(0).GetVal() != gameState.GetHand("player").GetCard(1).GetVal()))
                        {
                            Console.WriteLine("Splitting is not allowed\n");
                            break;
                        }
                        SplitHand(gameState.GetHand("player"), gameState.GetHand("doubles"));
                        DealKnown(gameState.GetHand("doubles"), gameState);
                        return DealKnown(gameState.GetHand("player"), gameState);
                    //hit and increase the bet
                    case ("double"):
                        gameState.IncrementPot();
                        return DealKnown(gameState.GetHand("player"), gameState);
                    //split the hand and increase the bet
                    case ("doublesplit"):
                        if (gameState.GetHand("player").GetCardCount() > 2 || (gameState.GetHand("player").GetCard(0).GetVal() != gameState.GetHand("player").GetCard(1).GetVal()))
                        {
                            Console.WriteLine("Splitting is not allowed\n");
                            break;
                        }
                        gameState.IncrementPot();
                        SplitHand(gameState.GetHand("player"), gameState.GetHand("doubles"));
                        DealKnown(gameState.GetHand("doubles"), gameState);
                        return DealKnown(gameState.GetHand("player"), gameState);
                    default:
                        return false;
                }
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
            return p.BasicStrat(hand.GetCard(0).GetVal(),hand.GetCard(1).GetVal(),hand.HandValueInt(), dealerHand.GetCard(0).GetVal(), true);

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

        static bool Compare(Hand hand, Hand dealerHand, GameFeatures gameState, string input)
        {
            if (Decision(gameState.GetHand("player"), gameState.GetHand("dealer"), gameState) == input)
            {
                return true;
            }
            return false;
        }
    }
}
