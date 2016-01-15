using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeetBOT.NB_Classes
{
    public class SendMethods
    {
        public static string PrivateMessage(string message)
        {
            return "PRIVMSG " + MainWindow.CHANNEL + " :" + message;            
        }

        public static string UserPrivateMessage(string message, string user)
        {
            return "PRIVMSG " + user + " :" + message;
        }

        public static string ctcpVersionResponse(string user)
        {           
            return "NOTICE " + user + " :" + "\u0001" + "VERSION " + MainWindow.VERSION + "\u0001";
        }
    }
}
