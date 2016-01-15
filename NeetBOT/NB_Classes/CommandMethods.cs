using System;
using System.Collections.Generic;
using NCalc;
using System.Text;

namespace NeetBOT.NB_Classes
{
    public class CommandMethods
    {
        public static Random rand = new Random();

        public static List<string> Help()
        {
            List<string> helpLines = new List<string>()
            {
                SendMethods.PrivateMessage("-=-------=- [ NeetBOT Help ] -=-------=-"),
                SendMethods.PrivateMessage("~NB Math/Maths[calculation] - eg. Math[1337*420]"),
                SendMethods.PrivateMessage("~NB insult - Insult a random shit language")
            };
            return helpLines;
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
            List<string> langs = new List<string>() { "Haskell", "Ruby", "Java", "C", "Rust", "C#", "D", "PHP", "JavaScript" };
            List<string> negAdj = new List<string>() { "shit", "terrible", "worthless", "idiotic", "bloated", "horseshit", "utter wank", "awful"};
            List<string> negIns = new List<string>() { "cunts", "arsebandits", "morons", "nitwits", "babby-tier programmers", "dickheads" };
            return (SendMethods.PrivateMessage(langs[rand.Next(0, langs.Count)] + " is a " + negAdj[rand.Next(0, negAdj.Count)] + " language for " + negIns[rand.Next(0, negIns.Count)]));
        }

        public static List<string> ChallengeList;
        public static string Challenge()
        {          
            return (SendMethods.PrivateMessage("Your challenge - " + ChallengeList[rand.Next(0, ChallengeList.Count)] + " - Better get started."));
        }
        public static string ChallengeCount()
        {
            return (SendMethods.PrivateMessage("Number of Challenges -" + ChallengeList.Count));
        }
    }
}
