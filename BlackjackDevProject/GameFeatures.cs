using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    public class GameFeatures
    {
        private double pot = 0;
        private double bettingAmount = 100;
        private int intialBet = 2;
        private Deck deckInPlay;
        Hand playerHand = new Hand();
        Hand dealerHand = new Hand();
        Hand doublesHand = new Hand();

        public void SetPot(double inVal)
        {
            pot = inVal;
        }

        public void IncrementPot()
        {
            pot += intialBet;
        }

        public double GetPot()
        {
            return pot;
        }

        public void SetBettingAmount(int inVal)
        {
            bettingAmount = inVal;
        }

        public void ChangeBettingAmount(bool win)
        {
            if(win)
            {
                bettingAmount += pot;
            }
            else
            {
                bettingAmount -= pot;
            }
        }

        public double GetBettingAmount()
        {
            return bettingAmount;
        }

        public void SetInitialBet(int inVal)
        {
            intialBet = inVal;
        }

        public int GetIntialBet()
        {
            return intialBet;
        }

        public void SetDeck(Deck inDeck)
        {
            deckInPlay = inDeck;
        }

        public Deck GetDeck()
        {
            return deckInPlay;
        }

        public Hand GetHand(string type)
        {
            if (type == "player")
            {
                return playerHand;
            }
            if(type == "dealer")
            {
                return dealerHand;
            }
            else
            {
                return doublesHand;
            }
        }
    }
}
