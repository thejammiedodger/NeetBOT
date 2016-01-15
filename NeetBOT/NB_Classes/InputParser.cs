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
            if (input.Contains(":~NB"))
            {
                ParseCommands(input);
            }
            else if(input.Contains("\u0001"))
            {
                ParseCTCP(input);
            }
        }

        public static void ParseCommands(string input)
        {
            string message = input.Substring(input.IndexOf("~NB"));
            if (StringContains(message, "maths") || StringContains(message, "math"))
            {
                ircWriteLine(CommandMethods.Maths(message));
            }
            else if (StringContains(message, "help"))
            {
                ircWriteMultiLine(CommandMethods.Help());
            }
            else if (StringContains(message, "insult"))
            {
                ircWriteLine(CommandMethods.Insult());
            }
            else if (StringContains(message, "challenge"))
            {
                ircWriteLine(CommandMethods.Challenge());
            }
            else
            {
                ircWriteLine(SendMethods.PrivateMessage("Invalid Command - Use '~NB Help'"));
            }
        }

        public static void ParseCTCP(string input)
        {
            int start = input.IndexOf(":") + 1;
            int length = input.IndexOf("!") - 1;
            string replyTo = input.Substring(start, length);
            if(input.Contains("VERSION"))
            {
                ircWriteLine(SendMethods.ctcpVersionResponse(replyTo));
            }
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
