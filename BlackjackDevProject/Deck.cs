using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    public class Deck
    {
        //List of all the cards
        private List<Card> cards = new List<Card>();
        //deck index value
        private int indexVal = 0;

        //Custom contructor
        public Deck()
        {
            //Calls the card constructor 52 times - once for each card
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 14; j++)
                {
                    cards.Add(new Card(j, i));
                }
            }
            List<Card> temp = cards;
            cards = Shuffle(cards);
            //i is the number of decks in play -> this number may be a var depending on later developments
            for (int i = 0; i < 2; ++i)
            {
                temp = Shuffle(temp);
                cards.AddRange(temp);
            }
            //shuffles the deck

        }

        //Returns the top card after removing it from the deck
        public Card GetCard()
        {
            Card c = cards[0];
            cards.Remove(cards[0]);
            return c;
        }

        //returns the current
        public List<Card> GetDeck()
        {
            return cards;
        }

        //used to shuffle the deck
        public List<Card> Shuffle(List<Card> deck)
        {
            //create a random number generator and a value to keep track of the number of cards untouched
            Random rng = new Random();
            int i = deck.Count();
            //while there is still cards to be switched
            while (i > 1)
            {
                //decrement
                --i;
                //find a random position within the deck
                int pos = rng.Next(i + 1); 
                //save the card inside it
                Card store = deck[pos];
                //swap the cards positions
                deck[pos] = deck[i];
                deck[i] = store;
            }
            return deck;
        }

        public void EditIndexVal(Card c)
        {
            int i = c.GetVal();
            if(i > 9)
            {
                ++indexVal;
                return;
            }
            if(i < 7)
            {
                --indexVal;
                return;
            }
        }

        public int GetIndexValue()
        {
            return indexVal;
        }
    }
}
