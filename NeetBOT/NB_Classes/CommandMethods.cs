using System;
using System.Collections.Generic;
using NCalc;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Linq;

namespace NeetBOT.NB_Classes
{
    public class CommandMethods
    {
        public static Random rand = new Random();

        #region Help Command
        public static List<string> Help(string user)
        {
            List<string> helpLines = new List<string>()
            {
                "-=-------=- [ NeetBOT Help ] -=-------=-",
                "!NB Math/Maths[calculation] - eg. Math[1337*420]",
                "!NB rotEncrypt[encrypt / decrypt | int | string] - eg. rot[encrypt | 13 | /dpt/ is shit]",
                "!NB insult - Insult a random shit language",
                "!NB challenge - Sends you a random challenge.",
                "----[count] - Sends the total number of challenges available.",
                "----[Add | string] - Adds a new challenge to the list",
                "!NB contribute - NeetBOT's github page."
            };

            List<string> tempHelp = new List<string>();
            foreach(var helpLine in helpLines)
            {
                tempHelp.Add(SendMethods.UserNotice(helpLine, user));
            }
            return tempHelp;
        }
        #endregion

        #region Maths command
        public static string Maths(string input)
        {
            if (input.Contains("[") && input.Contains("]"))
            {
                string calcString = input.Split('[', ']')[1];
                try
                {
                    Expression e = new Expression(calcString);
                    Object result = e.Evaluate();
                    return SendMethods.PrivateMessage("Result - " + calcString + " = " + result);
                }
                catch
                {
                    return SendMethods.PrivateMessage("Inavlid Parameter - Check your calculation");
                }
            }

            else
            {
                return SendMethods.PrivateMessage("Invalid Parameter - See !NB help");
            }
        }
        #endregion

        #region Insult Command
        public static InsultData insultData;


        public static string Insult(string input)
        {
            string ret = "";
            if (!(input.Contains("[") && input.Contains("]")))
            {
                string Lang = insultData.Languages[rand.Next(0, insultData.Languages.Count)];
                string Adj = insultData.Adjectives[rand.Next(0, insultData.Adjectives.Count)];
                string Ins = insultData.Insults[rand.Next(0, insultData.Insults.Count)];

                return (SendMethods.PrivateMessage(Lang + " is a " + Adj + " language for " + Ins));

            }

            if(!InputParser.StringContains(tempParams[0], "add"))
            {
                return (SendMethods.PrivateMessage("Invalid parameter - See !NB help"));

            }

            List<string> tempParams = InputParser.ParseParams(input);


            if(InputParser.StringContains(tempParams[1], "language"))
            {
                if (insultData.Languages.Any(x => x.Contains(tempParams[2])))
                {
                    insultData.Languages.Add(tempParams[2]);
                    ret =  (SendMethods.PrivateMessage("Language \"" + tempParams[2] + "\" added."));
                }
                else
                {
                    ret = (SendMethods.PrivateMessage("Language \"" + tempParams[2] + "\" already exists."));
                }
            }else if (InputParser.StringContains(tempParams[1], "adjective"))
            {
                if(insultData.Adjectives.Any(x => x.Contains(tempParams[2])))
                {
                    insultData.Adjectives.Add(tempParams[2]);
                    ret = (SendMethods.PrivateMessage("Adjective \"" + tempParams[2] + "\" added."));
                }
                else
                {
                    ret = (SendMethods.PrivateMessage("Adjective \"" + tempParams[2] + "\" already exists."));
                }

            } else if (InputParser.StringContains(tempParams[1], "insult"))
            {
                if (insultData.Adjectives.Any(x => x.Contains(tempParams[2])))
                {
                    insultData.Insults.Add(tempParams[2]);
                    ret = (SendMethods.PrivateMessage("Insult \"" + tempParams[2] + "\" added."));
                }
                else
                {
                    ret = (SendMethods.PrivateMessage("Insult \"" + tempParams[2] + "\" already exists."));
                }
            }
            else
            {
                ret = (SendMethods.PrivateMessage("Invalid parameter - See !NB help"));
            }

            return ret;
        }
        #endregion

        #region Challlenge Command
        public static ObservableCollection<string> ChallengeList;
        public static string Challenge(string user, string input)
        {
            if (input.Contains("[") && input.Contains("]"))
            {
                List<string> tempParams = InputParser.ParseParams(input);

                if(InputParser.StringContains(tempParams[0],"count"))
                {
                    return (SendMethods.PrivateMessage("Number of Challenges - " + ChallengeList.Count));
                }
                else if(InputParser.StringContains(tempParams[0], "add") && tempParams.Count == 2)
                {
                    try { ChallengeList.Add(tempParams[1]); return SendMethods.PrivateMessage("Challenge \"" + tempParams[1] + "\" Added."); }
                    catch { return SendMethods.PrivateMessage("Invalid challenge - See !NB Help"); }
                }
                else
                {
                    return (SendMethods.PrivateMessage("Invalid Challenge Parameter - See !NB Help"));
                }

            }
            else
            {
                return (SendMethods.UserNotice("Your challenge - " + ChallengeList[rand.Next(0, ChallengeList.Count)] + " - Better get started.", user));
            }
        }
        #endregion

        #region Uptime Command
        public static string Uptime()
        {
            TimeSpan duration = DateTime.Now - MainWindow.startTime;
            return (SendMethods.PrivateMessage(string.Format("Current uptime - {0:%d} Days {0:%h} Hours {0:%m} Minutes and {0:%s} Seconds.", duration)));
        }
        #endregion

        #region Rot Encryption Command
        public static string RotEncrypt(string input)
        {
            if (input.Contains("[") && input.Contains("]"))
            {
                List<string> tempParams = InputParser.ParseParams(input);
                if (InputParser.StringContains(tempParams[0], "encrypt"))
                {
                    try
                    {
                        int rotVal = Int32.Parse(tempParams[1]);
                        string message = tempParams[2];
                        if (rotVal <= 25)
                        {
                            char[] inArray = message.ToCharArray();
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < inArray.Length; i++)
                            {
                                int c = inArray[i];
                                if (c >= 'a' && c <= 'z')
                                {
                                    c = (c - 'a' + 26 + rotVal) % 26 + 'a';
                                }
                                else if (c >= 'A' && c <= 'Z')
                                {
                                    c = (c - 'A' + 26 + rotVal) % 26 + 'A';
                                }
                                else { }
                                inArray[i] = (char)c;
                                sb.Append(inArray[i].ToString());
                            }
                            return (SendMethods.PrivateMessage("Rot" + rotVal + " Output: " + sb.ToString()));
                        }
                        else
                        {
                            return (SendMethods.PrivateMessage("Rotation value too high - Max: 25"));
                        }
                    }
                    catch
                    {
                        return (SendMethods.PrivateMessage("Invalid Parameter - See !NB Help"));
                    }
                }
                else if (InputParser.StringContains(tempParams[0], "decrypt"))
                {
                    try
                    {
                        int rotVal = Int32.Parse(tempParams[1]);
                        string message = tempParams[2];
                        if (rotVal <= 25)
                        {
                            char[] inArray = message.ToCharArray();
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < inArray.Length; i++)
                            {
                                int c = inArray[i];
                                if (c >= 'a' && c <= 'z')
                                {
                                    c = (c - 'a' + 26 - rotVal) % 26 + 'a';
                                }
                                else if (c >= 'A' && c <= 'Z')
                                {
                                    c = (c - 'A' + 26 - rotVal) % 26 + 'A';
                                }
                                else { }
                                inArray[i] = (char)c;
                                sb.Append(inArray[i].ToString());
                            }
                            return (SendMethods.PrivateMessage("Rot" + rotVal + " Output: " + sb.ToString()));
                        }
                        else
                        {
                            return (SendMethods.PrivateMessage("Rotation value too high - Max: 25"));
                        }
                    }
                    catch
                    {
                        return (SendMethods.PrivateMessage("Invalid Parameter - See !NB Help"));
                    }
                }
                else
                {
                    return (SendMethods.PrivateMessage("Invalid Parameter - See !NB Help"));
                }
            }
            else
            {
                return (SendMethods.PrivateMessage("Invalid Parameter - See !NB Help"));
            }
        }
        #endregion
    }
}
