using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeetBOT.NB_Classes
{
    public class InputParser
    {

        public static void Parse(string input)
        {
            if (input.Contains(":!NB"))
            {
                ParseCommands(input);
            }
            else if (input.Contains("\u0001"))
            {
                ParseCTCP(input);
            }
        }

        public static void ParseCommands(string input)
        {


            string message = input.Substring(input.IndexOf("!NB"));
            string userRequested = input.Substring(input.IndexOf(":") + 1, input.IndexOf("!") - 1);

            if (StringContains(message, "maths") || StringContains(message, "math"))
            {
                ircWriteLine(CommandMethods.Maths(message));
            }
            else if (StringContains(message, "help"))
            {
                ircWriteMultiLine(CommandMethods.Help(userRequested));
            }
            else if (StringContains(message, "insult"))
            {
                ircWriteLine(CommandMethods.Insult());
            }
            else if (StringContains(message, "challenge"))
            {
                ircWriteLine(CommandMethods.Challenge(userRequested, input));
            }
            else if (StringContains(message, "imply"))
            {
                ircWriteLine(SendMethods.PrivateMessage("\u0003" + "03>Implying"));
            }
            else if (StringContains(message, "contribute"))
            {
                ircWriteLine(SendMethods.UserPrivateMessage("https://github.com/sirdoombox/NeetBOT", userRequested));
            }
            else if (StringContains(message, "uptime"))
            {
                ircWriteLine(CommandMethods.Uptime());
            }
            else if (StringContains(message, "rotencrypt"))
            {
                ircWriteLine(CommandMethods.RotEncrypt(message));
            }
            else if (StringContains(message, "rotdecrypt"))
            {
                ircWriteLine(CommandMethods.RotEncrypt(message));
            }
            else
            {
                ircWriteLine(SendMethods.PrivateMessage("Invalid Command - Use '~NB Help'"));
            }
        }

        public static void ParseCTCP(string input)
        {
            string userRequested = input.Substring(input.IndexOf(":") + 1, input.IndexOf("!") - 1);
            if (input.Contains("VERSION"))
            {
                ircWriteLine(SendMethods.ctcpVersionResponse(userRequested));
            }
        }

        public static List<string> ParseParams (string message)
        {
            List<string> tempParams = new List<string>();
            string args = message.Split('[', ']')[1];
            string[] tempStrings = args.Split('|');
            foreach(string param in tempStrings)
            {
                tempParams.Add(param);
            }
            return tempParams;
        }

        public static bool StringContains(string message, string word)
        {
            return message.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static void ircWriteLine(string output)
        {
            MainWindow.writer.WriteLine(output);         
        }
        public static void ircWriteMultiLine(List<string> output)
        {
            foreach(string line in output)
            {
                MainWindow.writer.WriteLine(line);
            }
        }
    }
}
