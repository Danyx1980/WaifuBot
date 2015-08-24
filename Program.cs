using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading; 

namespace WaifuBot
{
    class WaifuBot
    {
        //Initializing global variables
        public static string SERVER = "irc.freenode.net"; //Server to use
        private static int PORT = 6667; //Default port
        private static string USER = "USER WaifuBot 0 * :Rin Tohsaka";  //Something something standard 
        private static string NICK = "WaifuBot"; //Nick
        public static string CHANNEL = "#/r/OreGairuSNAFU"; //Channel
        //public static string CHANNEL = "#oregairusnafu2";
        public static StreamWriter writer;

        static void Main(string[] args)
        {
            NetworkStream stream;
            TcpClient irc;
            string inputLine;
            StreamReader reader;
            string[] splitInput;
            ChatHandler handler = new ChatHandler();
            string response;

            try
            {
                //Connecting to IRC server and channel
                irc = new TcpClient(SERVER, PORT);
                stream = irc.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.WriteLine("NICK " + NICK);
                writer.Flush();
                writer.WriteLine(USER);
                writer.Flush();
                writer.WriteLine("JOIN " + CHANNEL);
                writer.Flush();

                while(true)
                {
                    while((inputLine = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(inputLine);
                        splitInput = inputLine.Split(new Char[] { ' ' });

                        if (inputLine.Substring(0, 4) == "PING")
                        {
                            string PongReply = inputLine.Substring(6); 
                            Console.WriteLine("->PONG " + PongReply);
                            writer.WriteLine("PONG " + PongReply);
                            writer.Flush();
                            continue; 
                        }

                        response = handler.Response(inputLine);
                        if (response != null)
                        {
                            if (response.Contains("\n"))
                            {
                                string[] multiLineResponse = response.Split(new Char[] { '\n' });

                                foreach (string message in multiLineResponse)
                                {
                                    writer.WriteLine(string.Format("PRIVMSG {0} :{1}", CHANNEL, message));
                                    writer.Flush();
                                }
                            }
                            else
                            {
                                writer.WriteLine(string.Format("PRIVMSG {0} :{1}", CHANNEL, response));
                                writer.Flush();
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Thread.Sleep(5000);
                string[] argv = { };
                Main(argv);
            }
        }

    }

}
