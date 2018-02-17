using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackDevProject;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HandValues()
        {
            Player p = new Player();

            //for every first card
            for (int i = 2; i < 12; i++)
            {
                //for every second card
                for (int j = 2; j < 12; j++)
                {
                    //for every dealer card
                    for (int k = 2; k < 12; k++)
                    {
                        Assert.AreEqual(Strat(i, j, i + j, k), p.BasicStrat(i, j, i + j, k));
                    }
                }
            }
        }

        // 0 = hit, 1 = stand, 2 = double, 3 = split, 4 = doublesplit

        //a = card1, b = card2, c = handval, d = dealerval
        private string Strat(int a, int b, int c, int d)
        {
            //if doubles
            if (a == b)
            {
                switch (c)
                {
                    case (4):
                        {
                            if (d <= 3)
                            {
                                return "doubleSplit";
                            }
                            if (d >= 4 && d <= 7)
                            {
                                return "split";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (6):
                        {
                            if (d <= 3)
                            {
                                return "doubleSplit";
                            }
                            if (d >= 4 && d <= 7)
                            {
                                return "split";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (8):
                        {
                            if (d == 5 || d == 6)
                            {
                                return "doubleSplit";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (12):
                        {
                            if (d == 2)
                            {
                                return "doubleSplit";
                            }
                            if (d >= 2 && d <= 6)
                            {
                                return "split";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (14):
                        {
                            if (d >= 8)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "split";
                            }
                        }
                    case (16):
                        {
                            return "split";
                        }
                    case (18):
                        {
                            if (d == 7 | d >= 10)
                            {
                                return "stand";
                            }
                            else
                            {
                                return "split";
                            }
                        }
                    case (22):
                        {
                            return "split";
                        }
                }
            }
            //if A xor B are aces
            if (a == 11 ^ b == 11)
            {
                switch (c)
                {
                    case (13):
                        {
                            if (d == 5 || d == 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (14):
                        {
                            if (d == 5 || d == 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (15):
                        {
                            if (d >= 4 && d <= 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (16):
                        {
                            if (d >= 4 && d <= 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (17):
                        {
                            if (d >= 3 && d <= 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (18):
                        {
                            if (d >= 4 && d <= 6)
                            {
                                return "double";
                            }
                            if (d >= 9)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "stand";
                            }
                        }
                    default:
                        {
                            return "stand";
                        }
                }
            }
            else
            {
                if (c <= 8)
                {
                    return "hit";
                }
                switch (c)
                {
                    case (9):
                        {
                            if (d >= 3 && d <= 6)
                            {
                                return "double";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (10):
                        {
                            if (d >= 10)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "double";
                            }
                        }
                    case (11):
                        {
                            if (d == 11)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "double";
                            }
                        }
                    case (12):
                        {
                            if (d >= 4 && d <= 6)
                            {
                                return "stand";
                            }
                            else
                            {
                                return "hit";
                            }
                        }
                    case (13):
                        {
                            if (d >= 7)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "stand";
                            }
                        }
                    case (14):
                        {
                            if (d >= 7)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "stand";
                            }
                        }
                    case (15):
                        {
                            if (d >= 7)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "stand";
                            }
                        }
                    case (16):
                        {
                            if (d >= 7)
                            {
                                return "hit";
                            }
                            else
                            {
                                return "stand";
                            }
                        }
                    default:
                        {
                            return "stand";
                        }
                }
            }
        }
    }
}
