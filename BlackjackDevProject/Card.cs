using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackDevProject
{
    //Class used to create all the cards
    class Card
    {
        //Stores the cards value and suit. A,J,Q,K are represented by their numerical equivalents
        private int value;
        private int suit;

        //Constructor to make the card
        public Card(int i, int s)
        {
            value = i;
            suit = s;
        }

        //Returns the cards numerical value when needed
        public int GetVal()
        {
            //J,Q,Ks all have a value of 10 numerically
            if (value > 10)
            {
                return 10;
            }
            if (value == 1)
            {
                return 11;
            }
            return value;
        }

        //Returns the cards actual value
        public int GetTrueValue()
        {
            return value;
        }

        //Custom ToString for printing out the cards
        public override string ToString()
        {
            string val;
            string s;

            //Switch case that turns the numerical value into the picture equivalents in the required cases
            switch (value)
            {
                case 1:
                    val = "A";
                    break;

                case 11:
                    val = "J";
                    break;

                case 12:
                    val = "Q";
                    break;

                case 13:
                    val = "K";
                    break;

                default:
                    val = value.ToString();
                    break;
            }

            //Switch case that turns the suit int into an actual suit
            switch (suit)
            {
                case 0:
                    s = "♥";
                    break;

                case 1:
                    s = "♦";
                    break;

                case 2:
                    s = "♠";
                    break;

                case 3:
                    s = "♣";
                    break;

                default:
                    s = "Error Assigning Suit";
                    break;
            }

            //Format the return
            return val + s;
        }
    }
}
