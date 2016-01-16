using System;
using System.Collections.Generic;
using NCalc;
using System.Text;
using System.Text.RegularExpressions;

namespace NeetBOT.NB_Classes
{
    public class CommandMethods
    {
        public static Random rand = new Random();

        public static List<string> Help(string user)
        {
            List<string> helpLines = new List<string>()
            {
                "-=-------=- [ NeetBOT Help ] -=-------=-",
                "!NB Math/Maths[calculation] - eg. Math[1337*420]",
                "!NB rotEncrypt[int | string] - eg. rot[13, /dpt/ is shit]",
                "!NB rotdecrypt[int | string]",
                "!NB insult - Insult a random shit language",
                "!NB challenge - Sends you a random challenge.",
                "----[count] - Sends the total number of challenges available.",
                "!NB contribute - NeetBOT's github page."
            };

            List<string> tempHelp = new List<string>();
            foreach(var helpLine in helpLines)
            {
                tempHelp.Add(SendMethods.UserNotice(helpLine, user));
            }
            return tempHelp;
        }

        public static string Maths(string message)
        {
            if (message.Contains("[") && message.Contains("]"))
            {
                string calcString = message.Split('[', ']')[1];
                try
                {
                    Expression e = new Expression(calcString);
                    Object result = e.Evaluate();
                    return SendMethods.PrivateMessage("Result - " + calcString + " = " + result);
                }
                catch
                {
                    return SendMethods.PrivateMessage("Inavlid Parameter - Check your sum");
                }                   
            }

            else
            {
                return SendMethods.PrivateMessage("No Paramaters - Put your sum in '[ ]'");
            }
        }

        public static string Insult()
        {            
            List<string> langs = new List<string>() { "Haskell", "Ruby", "Java", "C", "Rust", "C#", "D", "PHP", "JavaScript", "Lua", "Python", "Common Lisp", "Perl" };
            List<string> negAdj = new List<string>() { "shit", "terrible", "worthless", "idiotic", "bloated", "horseshit", "utter wank", "awful", "great", "perfect" };
            List<string> negIns = new List<string>() { "cunts", "arsebandits", "morons", "nitwits", "babby-tier programmers", "dickheads" };
            return (SendMethods.PrivateMessage(langs[rand.Next(0, langs.Count)] + " is a " + negAdj[rand.Next(0, negAdj.Count)] + " language for " + negIns[rand.Next(0, negIns.Count)]));
        }

        public static List<string> ChallengeList;
        public static string Challenge(string user, string input)
        {                       
            if (input.Contains("[") && input.Contains("]"))
            {
                string args = input.Split('[', ']')[1];
                return (SendMethods.PrivateMessage("Number of Challenges - " + ChallengeList.Count));
            }
            else
            {
                return (SendMethods.UserNotice("Your challenge - " + ChallengeList[rand.Next(0, ChallengeList.Count)] + " - Better get started.", user));
            }                                
        }

        public static string Uptime()
        {
            TimeSpan duration = DateTime.Now - MainWindow.startTime;
            return (SendMethods.PrivateMessage("Current uptime - " + duration.ToString()));
        }

        public static string RotEncrypt(string input)
        {
            if (input.Contains("[") && input.Contains("]"))
            {
                List<string> tempParams = InputParser.ParseParams(input);
                try
                {
                    int rotVal = Int32.Parse(tempParams[0]);
                    string message = tempParams[1];
                    if (rotVal <= 25)
                    {
                        char[] array = message.ToCharArray();
                        for (int i = 0; i < array.Length; i++)
                        {
                            int number = (int)array[i];

                            if (number >= 'a' && number <= 'z')
                            {
                                if (number > 'm')
                                {
                                    number -= rotVal;
                                }
                                else
                                {
                                    number += rotVal;
                                }
                            }
                            else if (number >= 'A' && number <= 'Z')
                            {
                                if (number > 'M')
                                {
                                    number -= rotVal;
                                }
                                else
                                {
                                    number += rotVal;
                                }
                            }
                            array[i] = (char)number;
                        }
                        return (SendMethods.PrivateMessage("Rot" + rotVal + " Output: " + new string (array)));
                    }
                    else
                    {
                        return (SendMethods.PrivateMessage("Rotation value too high - Max: 25"));
                    }

                }
                catch
                {
                    return (SendMethods.PrivateMessage("Invalid Command - Please refer to '!NB Help'"));
                }                
            }
            return (SendMethods.PrivateMessage("Invalid Command - Please refer to '!NB Help'"));
        }

        public static string RotDecrypt(string input)
        {
            if (input.Contains("[") && input.Contains("]"))
            {
                List<string> tempParams = InputParser.ParseParams(input);
                try
                {
                    int rotVal = Int32.Parse(tempParams[0]);
                    string message = tempParams[1];
                    if (rotVal <= 25)
                    {
                        char[] array = message.ToCharArray();
                        for (int i = 0; i < array.Length; i++)
                        {
                            int number = (int)array[i];

                            if (number >= 'a' && number <= 'z')
                            {
                                if (number > 'm')
                                {
                                    number += rotVal;
                                }
                                else
                                {
                                    number -= rotVal;
                                }
                            }
                            else if (number >= 'A' && number <= 'Z')
                            {
                                if (number > 'M')
                                {
                                    number += rotVal;
                                }
                                else
                                {
                                    number -= rotVal;
                                }
                            }
                            array[i] = (char)number;
                        }
                        return (SendMethods.PrivateMessage("Rot" + rotVal + " Output: " + new string(array)));
                    }
                    else
                    {
                        return (SendMethods.PrivateMessage("Rotation value too high - Max: 25"));
                    }

                }
                catch
                {
                    return (SendMethods.PrivateMessage("Invalid Command - Please refer to '!NB Help'"));
                }
            }
            return (SendMethods.PrivateMessage("Invalid Command - Please refer to '!NB Help'"));
        }
    }
}
