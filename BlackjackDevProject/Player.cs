using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace BlackjackDevProject
{
    public class Player
    {
        private string[] arr = new string[350];
        private int indexVal = -10;

        public Player(int inVal)
        {
            StreamReader read = new StreamReader("rules.txt");
            for (int i = 0; i < 350; ++i)
            {
                arr[i] = read.ReadLine();
            }
            indexVal = inVal;
        }

        public Player()
        {
            StreamReader read = new StreamReader("rules.txt");
            for (int i = 0; i < 350; ++i)
            {
                arr[i] = read.ReadLine();
            }
        }

        //basic strategy ai
        public string basicStrat(int card1, int card2, int handVal, int dVal)
        {
            string doubles = "([1-9][01]?),([1-9][01]?)/([0-9][0-9]?)/([0-9][0-9]?)/([a-zA-Z]+)/?([a-z]+)?/?([+-][0-5])?";
            string soft = "-,A/([0-9][0-9]?)/([0-9][0-9]?)/([a-zA-Z]+)/?([a-z]+)?/?([+-][0-5])";
            string hard = "-,-/([0-9][0-9]?)/([0-9][0-9]?)/([a-zA-Z]+)/?([a-z]+)?/?([+-][0-5])";
            for (int i = 0; i < 350; ++i)
            {
                if (card1 == card2)
                {
                    foreach (Match m in Regex.Matches(arr[i], doubles))
                    {
                        if (m.Groups[1].ToString() == card1.ToString() && m.Groups[2].ToString() == card2.ToString() && m.Groups[3].ToString() == (handVal).ToString() && m.Groups[4].ToString() == dVal.ToString())
                        {
                            if(m.Groups.Count == 7)
                            {
                                return HiLoAI(m.Groups[6].ToString(), int.Parse(m.Groups[7].ToString()));
                            }
                            return m.Groups[5].ToString();
                        }
                    }
                }
                if(card1 == 11 ^ card2 == 11)
                {
                    foreach (Match m in Regex.Matches(arr[i], soft))
                    {
                        if (m.Groups[1].ToString() == (handVal).ToString() && m.Groups[2].ToString() == dVal.ToString())
                        {
                            return m.Groups[3].ToString();
                        }
                    }
                }
                else
                {
                    foreach (Match m in Regex.Matches(arr[i], hard))
                    {
                        if (m.Groups[1].ToString() == (handVal).ToString() && m.Groups[2].ToString() == dVal.ToString())
                        {
                            return m.Groups[3].ToString();
                        }
                    }
                }
            }
            return "error : hand not recognised";
        }

        public string HiLoAI(string a, int index)
        {
            if (indexVal > index)
            {
                return a;
            }
            return "hit";
        }

        //odds ai -> tries to get as high as possible -> needs list of known cards
        public string odds(int card1, int card2)
        {
            return "";
        }

        //ai that tries to get over a certain value (17)
        public string valueBased(int val)
        {
            if(val > 17)
            {
                return "stand";
            }
            else
            {
                return "hit";
            }
        }

        //random ai
        public string random()
        {
            Random rnd = new Random();
            if(rnd.Next(0,2) == 0)
            {
                return "stand";
            }
            else
            {
                return "hit";
            }
        }

        
    }
}
