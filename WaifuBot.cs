using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic; 
using MongoDB.Bson;
using MongoDB.Driver; 

namespace WaifuBot
{
    class WaifuBot
    {
        //Initializing global variables
        public static string SERVER = "irc.freenode.net"; //Server to use
        private static int PORT = 6667; //Default port
        private static string USER = "USER BestGirl 0 * :Rin Tohsaka";  //Something something standard 
        public static string NICK = "WaifuBot"; //Nick
        //public static string NICK = "TestoBoto";
        public static string CHANNEL = "#/r/OreGairuSNAFU"; //Channel
        public static string CONTROL = "\x01";  
        //public static string CHANNEL = "#oregairusnafu2";
        public static NetworkStream stream;

        static void Main(string[] args)
        {
            TcpClient irc;
            string inputLine;
            StreamWriter writer;
            StreamReader reader;
            string[] splitInput;
            ChatHandler handler = new ChatHandler();
            PingSender pingSender;
            List<string> response;

            try
            {
                //Connecting to IRC server and channel
                irc = new TcpClient(SERVER, PORT);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                pingSender = new PingSender();
                writer.WriteLine("NICK " + NICK);
                writer.Flush();
                writer.WriteLine(USER);
                writer.Flush();
                writer.WriteLine("JOIN " + CHANNEL);
                writer.Flush();
                pingSender.Start(); 

                while(true)
                {
                    while((inputLine = reader.ReadLine()) != null)
                    {
                        splitInput = inputLine.Split(new Char[] { ' ' });
                        
                        if(splitInput[0] != "PONG")
                            Console.WriteLine(inputLine);

                        response = handler.Response(inputLine);
                        if (response != null)
                        {
                           
                            if (response[1].Contains("\n"))
                            {
                                string[] multiLineResponse = response[1].Split(new Char[] { '\n' });

                                foreach (string message in multiLineResponse)
                                {
                                    if (message.Contains("/me"))
                                    {
                                    }
                                    else
                                    {
                                        writer.WriteLine(string.Format("PRIVMSG {0} :{1}", response[0], message));
                                        writer.Flush();
                                    }
                                }
                            }

                            else if(response[1].Contains("/me"))
                            {
                                //writer.WriteLine(string.Format("PRIVMSG {0} \x01ACACTION {1}\x01AC"), CHANNEL, response);
                                //writer.Flush(); 
                            }
                            else
                            {
                                writer.WriteLine(string.Format("PRIVMSG {0} :{1}", response[0], response[1]));
                                writer.Flush();
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Thread.Sleep(15000);
                string[] argv = { };
                Main(argv);
            }
        }

    }

}
