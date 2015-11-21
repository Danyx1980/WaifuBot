using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
using System.Threading; 
using System.Threading.Tasks;
using System.Net; 
using System.Net.Sockets; 

namespace WaifuBot
{
    class PingSender
    {
        static string PING = "PING :";
        private Thread pingSender;
        private static NetworkStream stream = WaifuBot.stream;
        private StreamWriter writer = new StreamWriter(stream); 
        // Empty constructor makes instance of Thread
        public PingSender()
        {
            pingSender = new Thread(new ThreadStart(this.Run));
        }
        // Starts the thread
        public void Start()
        {
            pingSender.Start();
        }
        // Send PING to irc server every 15 seconds
        public void Run()
        {
            while (true)
            {
                writer.WriteLine(PING + WaifuBot.SERVER);
                writer.Flush();
                Thread.Sleep(15000);
            }
        }
    }
}
