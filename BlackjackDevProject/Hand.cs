using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    public class Hand
    {
        private List<Card> hand = new List<Card>();

        public Hand()
        {

        }

        //Adds a supplied card to the hand
        public void AddCard(Card c)
        {
            hand.Add(c);
        }

        //used for testing purposes
        public void PrintHand()
        {
            //write out each card
            foreach (Card c in hand)
            {
                Console.Write(c.ToString());
            }
            //and show the current total
            Console.Write(" Value: " + HandValueInt().ToString());
            Console.WriteLine();
        }

        //Returns whether the hand has gone bust or not 
        public bool HandValueBool()
        {
            int total = HandValueInt();
            if (total > 21)
            {
                return false;
            }
            return true;
        }

        //Returns the hands numerical value
        public int HandValueInt()
        {
            int total = 0;
            foreach (Card c in hand)
            {
                total += c.GetVal();
            }
            //if its bust but an ace exists, -10 so the ace is valued as a 1
            if(total > 21)
            {
                foreach (Card c in hand)
                {
                    if(c.GetVal() == 11)
                    {
                        total -= 10;
                    }
                    if(total < 21)
                    {
                        break;
                    }
                }
            }
            return total;
        }

        //return if the hand has a blackjack
        public bool BlackjackCheck()
        {
            //if either of the cards is a 1 and the other a picture card return true
            if ((hand[0].GetTrueValue() == 1 && hand[1].GetTrueValue() > 10) || (hand[1].GetTrueValue() == 1 && hand[0].GetTrueValue() > 10))
            {
                return true;
            }
            return false;
        }

        //Checks if the two cards dealt at the start are the same - only want to use this once ideally (not needed?)
        public bool DoubleCheck()
        {
            //if there are more then two cards its always false
            if(hand.Count > 2)
            {
                return false;
            }
            return hand[0].GetTrueValue() == hand[1].GetTrueValue();
        }

        //Gets a card in the hand
        public Card GetCard(int i)
        {
            return hand[i];
        }

        //Removes a card in the hand -> used for doubles
        public void RemoveCard(int i)
        {
            hand.RemoveAt(i);
        }

        //checks if there is any cards in the hand
        public bool IsEmpty()
        {
            if (hand.Count == 0)
            {
                return true;
            }
            return false;
        }

        //Removes all the cards in the hand
        public void Clear()
        {
            hand.Clear();
        }

        private bool AceCheck()
        {
            foreach (Card c in hand)
            {
                if (c.GetVal() == 11)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
