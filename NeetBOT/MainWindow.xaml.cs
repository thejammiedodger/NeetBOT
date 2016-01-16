﻿using System;
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

namespace NeetBOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string USER = "USER NeetBOT 0 * :I am NeetBOT";
        public static string NICK = "NeetBOT";

        public static string VERSION = "NeetBOT v0.1a - C# & .NET4.5.2";

        public static string SERVER;
        public static int PORT;
        public static string CHANNEL;

        public static DateTime startTime = new DateTime();
        

        public MainWindow()
        {
            InitializeComponent();

            CommandMethods.ChallengeList = JSON.Deserialise(LocalResources.challenges); lstChallenges.ItemsSource = CommandMethods.ChallengeList;
            BindingOperations.EnableCollectionSynchronization(lstChallenges.ItemsSource, CommandMethods.ChallengeList);

            txtChannel.Text = "DPT";
            txtPort.Text = "6667";
            txtServer.Text = "irc.rizon.net";

            startTime = DateTime.UtcNow;
        }

        #region Connection Controls
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectContainer.Visibility = Visibility.Collapsed;
            MainContainer.Visibility = Visibility.Visible;
            SERVER = txtServer.Text;
            PORT = Int32.Parse(txtPort.Text);
            CHANNEL = "#" + txtChannel.Text;
            IRC_Connect(SERVER, PORT, CHANNEL, USER, NICK);
        }
        #endregion

        #region IRC Connection
        public TcpClient irc;
        public StreamReader reader;
        public static StreamWriter writer;
        public async void IRC_Connect(string server, int port, string channel, string user, string nick)
        {
            NetworkStream stream;
            string inputLine;
            try
            {
                await Task.Run(async() =>
                {
                    irc = new TcpClient(server, port);
                    stream = irc.GetStream();
                    await Task.Delay(1000);
                    reader = new StreamReader(stream);
                    writer = new StreamWriter(stream);              
                    writer.WriteLine(user);
                    writer.Flush();
                    writer.WriteLine("NICK " + nick);
                    writer.Flush();
                    writer.WriteLine("JOIN " + channel);
                    writer.Flush();                   
                    while (true)
                    {
                        while ((inputLine = reader.ReadLine()) != null)
                        {
                            if (inputLine.Contains("PING :") && !inputLine.Contains("!"))
                            {
                                await Task.Delay(5000);
                                MainWindow.writer.WriteLine(inputLine.Replace("PING", "PONG"));
                                Console.WriteLine(inputLine.Replace("PING", "PONG"));
                            }
                            Console.WriteLine(inputLine);
                            InputParser.Parse(inputLine);
                            writer.Flush();                                                     
                        }
                        // Close all streams
                        writer.Close();
                        reader.Close();
                        irc.Close();
                    }
                });               
            }
            catch (Exception e)
            {
                // Show the exception, sleep for a while and try to establish a new connection to irc server
                Console.WriteLine(e.ToString());
                Thread.Sleep(5000);
                string[] argv = { };
                IRC_Connect(server, port, channel, user, nick);
            }
        }
        #endregion

        #region Misc code
        protected override void OnClosed(EventArgs e)
        {          
            reader.Close();
            irc.Close();
            writer.Close();
            base.OnClosed(e);           
            Application.Current.Shutdown();
        }
        #endregion

        private void lstChallenges_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(lstChallenges.SelectedIndex != -1)
            {
                if(e.Key == Key.Delete)
                {
                    CommandMethods.ChallengeList.RemoveAt(lstChallenges.SelectedIndex);
                }
            }
        }
    }
}
