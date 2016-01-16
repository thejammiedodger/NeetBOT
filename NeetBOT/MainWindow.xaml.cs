using System;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Net.Sockets;
using NeetBOT.NB_Classes;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using NeetBOT.Properties;
using System.Windows.Data;
using System.Windows.Input;
using System.Linq;
using System.Reflection;
using ChatSharp;

namespace NeetBOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string NAME = "NeetBOT";

        public static string VERSION = "NeetBOT v0.1a - C# & .NET4.5.2";

        public static string SERVER;
        public static int PORT;
        public static string CHANNEL;

        public static DateTime startTime = new DateTime();
        

        public MainWindow()
        {
            InitializeComponent();

            LoadLocal();

            txtChannel.Text = "DPT";
            txtPort.Text = "6667";
            txtServer.Text = "irc.rizon.net";

            startTime = DateTime.UtcNow;
        }

        #region LoadLocal
        private void LoadLocal()
        {
            Directory.CreateDirectory(LocalData.AppDataFolder);

            if(!Directory.EnumerateFiles(LocalData.AppDataFolder).Any())
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(LocalResources.challenges))
                using (StreamReader reader = new StreamReader(stream))
                {
                    File.WriteAllText(LocalData.challenges, reader.ReadToEnd());
                }

                using (Stream stream = assembly.GetManifestResourceStream(LocalResources.insults))
                using (StreamReader reader = new StreamReader(stream))
                {
                    File.WriteAllText(LocalData.insults, reader.ReadToEnd());
                }                
            }

            CommandMethods.ChallengeList = JSON.Deserialise(LocalData.challenges); lstChallenges.ItemsSource = CommandMethods.ChallengeList;
            BindingOperations.EnableCollectionSynchronization(lstChallenges.ItemsSource, CommandMethods.ChallengeList);

            CommandMethods.insultData = JSON.DeserialiseInsultData();
            lstLanguages.ItemsSource = CommandMethods.insultData.Languages; BindingOperations.EnableCollectionSynchronization(lstLanguages.ItemsSource, CommandMethods.insultData.Languages);
            lstAdj.ItemsSource = CommandMethods.insultData.Adjectives; BindingOperations.EnableCollectionSynchronization(lstAdj.ItemsSource, CommandMethods.insultData.Adjectives);
            lstIns.ItemsSource = CommandMethods.insultData.Insults; BindingOperations.EnableCollectionSynchronization(lstIns.ItemsSource, CommandMethods.insultData.Insults);
        }
        #endregion

        #region Connection Controls
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {          
            SERVER = txtServer.Text;
            PORT = Int32.Parse(txtPort.Text);
            CHANNEL = "#" + txtChannel.Text;
            IRC_Connect(SERVER, PORT, CHANNEL, NAME);
            ircConnected();
        }
        #endregion

        #region Main Controls
        
        #endregion

        #region Challenge controls
        private void lstChallenges_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (lstChallenges.SelectedIndex != -1)
            {
                if (e.Key == Key.Delete)
                {
                    CommandMethods.ChallengeList.RemoveAt(lstChallenges.SelectedIndex);
                }
            }
        }
        #endregion

        #region Insult controls
        private void lstLanguages_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstLanguages.SelectedIndex != -1)
            {
                if (e.Key == Key.Delete)
                {
                    CommandMethods.insultData.Languages.RemoveAt(lstLanguages.SelectedIndex);
                }
            }
        }

        private void lstIns_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstIns.SelectedIndex != -1)
            {
                if (e.Key == Key.Delete)
                {
                    CommandMethods.insultData.Insults.RemoveAt(lstIns.SelectedIndex);
                }
            }
        }

        private void lstAdj_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstAdj.SelectedIndex != -1)
            {
                if (e.Key == Key.Delete)
                {
                    CommandMethods.insultData.Adjectives.RemoveAt(lstAdj.SelectedIndex);
                }
            }
        }
        #endregion

        #region IRC Connection
        public static IrcClient _irc;
        public void IRC_Connect(string server, int port, string channel, string name)
        {
            _irc = new IrcClient(server, new IrcUser(name, name));
            _irc.ConnectAsync();
            _irc.ConnectionComplete += (s, e) => 
            {
                _irc.JoinChannel(channel);        
            };
            _irc.RawMessageRecieved += (s, e) => { InputParser.Parse(e.Message); };
        }
        #endregion
        
        #region Misc code

        private void ircConnected()
        {
            ConnectContainer.Visibility = Visibility.Collapsed;
            MainContainer.Visibility = Visibility.Visible;
            btnDisconnect.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
            ShowCloseButton = false;
        }

        public static string quitReason = string.Format("QUIT [Application Closed - Goodbye {0}]", CHANNEL);
        private async void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            _irc.Quit(quitReason);
            await Task.Delay(1000);
            btnDisconnect.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
            ConnectContainer.Visibility = Visibility.Visible;
            MainContainer.Visibility = Visibility.Collapsed;
        }

        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            _irc.Quit(quitReason);
            await Task.Delay(1000);
            this.Close();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            JSON.SerializeInsultData(CommandMethods.insultData);          
        }

        #endregion


    }
}
