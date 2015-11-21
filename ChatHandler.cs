using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver; 

namespace WaifuBot
{
    class ChatHandler
    {
        string nickname;
        string[] splitInput;
        string input = "";
        string sender; 
        private DBInterface dbinterface = new DBInterface();
        bool imIn = false; 

        public List<string> Response(string message)
        {
            List<string> response = new List<string>();
            splitInput = message.Split(new Char[] { ' ' });
            input = message; 

                if (imIn == true)
                {
                    nickname = GetNick();
                    GetSender();
                    response.Add(sender);
                }

            if(splitInput[1] == "JOIN")
            {
                if (splitInput[0].Contains(WaifuBot.NICK))
                {
                    imIn = true;

                    nickname = GetNick();
                    GetSender();
                    response.Add(sender);

                    response.Add(SelfJoin());
                    return response; 
                }

                else
                {
                    dbinterface.inserLogIn(JoinParser());
                    response.Add(UserJoin());
                    return response; 
                }
            }

            if (splitInput[1] == "QUIT" || splitInput[1] == "PART")
            {
                UserLeave(); 
            }

            if (splitInput[1] == "PRIVMSG")
            {
                input = message.Substring(message.IndexOf(" :")); 
                response.Add(GetResponse());
                response[0] = sender; 
                if (response[1] != null) return response;

                else return null; 
            }
            
            return null;
        }

        private string GetResponse()
        {
            string[] website = { ".com", ".moe", ".org", ".net", "http" };
            string[] possibleHate = { "fuck you", "i hate you", "you suck", "youre useless", "you're useless", "you useless", "nobody wants you", "get out", "fuck off" };
            string[] possibleSalutes = { "hello", "kombawa", "ohayo", "good morning", "morning", "hey", "ohayou" };
            string[] possibleLove = { "i love you", "you're so cool", "you so cool", "youre so cool", "you're awesome", "you awesome", "youre awesome", "is cool", "is cute", "is pretty", "are cute", "are pretty", "you're pretty", "you're so pretty", "youre pretty", "youre so pretty", "youre cute", "youre so cute", "you are so pretty", "you are so cute", "you are so cool", "i love", "is amazing", "is awesome", "you are awesome" };

            foreach (string substring in splitInput)
            {
                if (substring.ToLower().Contains("hentai"))
                {
                    foreach (string ignore in website)
                    {
                        if (substring.ToLower().Contains(ignore)) return null;
                    }
                return Hentai();
                }
            }

            foreach (string hate in possibleHate)
                if (input.ToLower().Contains(hate) && input.ToLower().Contains(WaifuBot.NICK.ToLower())) return Sad();

            foreach (string salutes in possibleSalutes)
                if (input.ToLower().Contains(salutes) && input.ToLower().Contains(WaifuBot.NICK.ToLower())) return Domo();

            if (splitInput[3].ToLower() == ":domo" && splitInput.Length == 4) return Domo(); 

            foreach (string love in possibleLove)
                if (input.ToLower().Contains(love) && input.ToLower().Contains(WaifuBot.NICK.ToLower())) return Love();

            if (input.ToLower().Contains("!calm"))
            {
                string substring = input.Substring(input.IndexOf("!calm"));
                try
                {
                    string[] substringsplit = substring.Split(new Char[] { ' ' });
                    if (substringsplit.Length > 1) nickname = substringsplit[1];
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString()); 
                }
                return Calm();
            }

            if (splitInput[3].ToLower().StartsWith(":ayy") && splitInput.Length == 4) return Lmao();

            if (splitInput[3].ToLower().Contains("!schedule")) return Schedule();

            if (splitInput[3].ToLower().Contains("!event")) return Event();

            if (splitInput[3].ToLower().Contains("!lastseen")) return LastSeen();

            if (splitInput[3].ToLower().Contains("!fullinfo"))
            {
                sender = nickname; 
                return FullInfo();
            }

            if (input.ToLower().Contains("!rules")) return Rules();

            else return null;
        }

        private string SelfJoin()
        {
            string[] selfJoinResponse = { "Ara?", "Tadaima", "I'm back!", "Darin! I'm back!" };
            Random answer = new Random();

            return selfJoinResponse[answer.Next(0, selfJoinResponse.Length)];
        }

        private string UserJoin()
        {
            string[] userJoinResponse = { "Ara? Welcome back!", "Okaerinasai", "Okaeri", "Welcome back darin!" };
            Random answer = new Random();

            if (answer.Next(0, 100) == 37) return "Welcome back darin! Would you want dinner? A bath? Or perhaps \r\n Me?";

            return userJoinResponse[answer.Next(0, userJoinResponse.Length)];
        }

        private void UserLeave()
        {
            List<string> leaveInfo = new List<string>();

            leaveInfo.Add(DateTime.Now.Date.ToString().Substring(0, 10));
            leaveInfo.Add(DateTime.Now.TimeOfDay.ToString().Substring(0, 8));
            leaveInfo.Add(nickname);

            dbinterface.inserLogOut(leaveInfo); 
        }

        private string Hentai()
        {
            string[] hentaiResponse = { "L-lewd!", "Uh- UUHH?!", "D-darin, you like those stuff?", "W-What are you talking about?!" , "Darin! I thought you were better than this!", "D-do you really like that? Am I not enough?", "Please! Let's keep the chat clean!"};
            Random answer = new Random();

            return hentaiResponse[answer.Next(0, hentaiResponse.Length)];
        }

        private string Domo()
        {
            string[] domoResponse = { "Ara? Ohayou darin ", "Good morning ", "Hmmm! I don't like being called that! you know? " };
            Random answer = new Random();

            if (nickname.ToLower().Contains("cult_films") && answer.Next(0, 20) == 13) return "Uh? Don't get near me, you creep";
            if (nickname.ToLower().Contains("anpan")) return (domoResponse[answer.Next(0, domoResponse.Length - 1)] + nickname);
            return (domoResponse[answer.Next(0, domoResponse.Length - 1)] + nickname + "-san");
        }

        private string Sad()
        {
            string[] hateResponses = { "Wow so mean, ", "/me cries \r\n Why so mean ", "I hve feelings too, you know? " };
            Random answer = new Random();

            if (nickname.ToLower().Contains("mahdi") && answer.Next(0, 10) == 5) return "No need to be so mean, Mahdi-san"; 
            else if (nickname.Contains("Rumi") && answer.Next(0, 10) == 3) return "Wow Rumi-chan, you're so mean :("; 
            else if(answer.Next(0, 999) == 337) return ("It's ok, I still love you " + nickname + "-san");

            return hateResponses[1]; 
            return (hateResponses[answer.Next(0, hateResponses.Length - 1)] + nickname + "-san"); 

        }

        private string Love()
        {
            string[] loveResponses = { "/me blushes", "Ara ara, you're making me blush!", "Darin, don't just say stuff like that!", "Oh darin!", "Oh darin! \r\n /me blushes" };
            Random answer = new Random();

            if (nickname.ToLower().Contains("naticus") && answer.Next(0, 10) == 7) return "Awww, stop it Lona-san!"; 
            if (nickname.ToLower().Contains("cult_films") && answer.Next(0, 50) == 34) return "Ew! get away from me, you freak!"; 
            if (answer.Next(0, 300) == 264) return "I already knew that, but thanks";

            return loveResponses[answer.Next(0, loveResponses.Length - 1)]; 

        }

        private string Calm()
        {
            string[] calmResponses = { "Ara ara, no need to be mad", "C'mon darin, sit here and relax", "Ara? Why are you so mad darin-kun?", "/me headpats \r\n Ara ara" };
            Random answer = new Random();

            if (nickname.ToLower().Contains("rumi") && answer.Next(0, 7) == 6) return "C'mon Rumi-chan, no need to be angry now";
            if (nickname.ToLower().Contains("mahdi") && answer.Next(0, 7) == 5) return "Now, now, Mahdi-san, don't let the salt get onto you";

            return calmResponses[answer.Next(0, calmResponses.Length - 1)]; 
        }

        private string Lmao()
        {
            string[] lmaoResponses = { "lmao" }; 
            Random answer = new Random();

            return lmaoResponses[answer.Next(0, lmaoResponses.Length - 1)]; 
        }

        private string Rules()
        {
            string[] rules = { "Post dongers.\n1. Praise Camilla. \n2. Remember Anna (Miyu Matsuki Sept. 14, 1977 - Oct. 27, 2015). \n3. Love Saki. \n4. Never talk about 9/7." };
            return rules[0]; 
        }

        private string Schedule()
        {
            string[] confirmation = { "Got it!", "Okay honey, I'll save that.", "Done!" };
            List<string> eventInfo = CommandHandler(); 
            Random answer = new Random();

            try
            {
                dbinterface.insertEvent(eventInfo);
                if(eventInfo.Count == 4)
                    return confirmation[answer.Next(0, confirmation.Length - 1)] + string.Format("\n{0} is scheduled for {1} at {2}. An additional note is attached, it says: {3}", eventInfo[0], eventInfo[1], eventInfo[2], eventInfo[3]);  
                else
                    return confirmation[answer.Next(0, confirmation.Length - 1)] + string.Format("\n{0} is now scheduled for {1} at {2}. ", eventInfo[0], eventInfo[1], eventInfo[2]);  
            }

            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return error(); 
            }
            
        }

        private string Event()
        {
            try
            {
                List<BsonDocument> eventInfo = dbinterface.getEvent(CommandHandler()[0]).Result;

                if(eventInfo.Last().Count() == 4)
                    return string.Format("{0} is scheduled for {1} at {2}.\n", eventInfo.Last()[1], eventInfo.Last()[2], eventInfo.Last()[3]); 

                else
                    return string.Format("{0} is scheduled for {1} at {2}. An additional note is attached, it says: {3}.\n", eventInfo.Last()[1], eventInfo.Last()[2], eventInfo.Last()[3], eventInfo.Last()[4]); 
                
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return error(); 
            }

        }

        private string GetNick()
        {
            int index = input.IndexOf("!");

            if (index >= 0) return input.Substring(1, index - 1);

            return null;
        }

        private string LastSeen()
        {
            try
            {
                List<BsonDocument> lastSeen = dbinterface.getLastSeen(splitInput[4]).Result;
                List<BsonDocument> logOut = dbinterface.getLogOut(splitInput[4]).Result;

                string answer = "";

                if (lastSeen != null) answer = string.Concat(answer, lastSeen.Last()[4] + " last connected on " + lastSeen.Last()[1] + " at " + lastSeen.Last()[2]); 

                if (logOut != null) answer = string.Concat(answer, "\n" + "Disconnected on " + logOut.Last()[1] + " at " + logOut.Last()[2]);

                return answer; 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return error(); 
            }
        }

        private string FullInfo()
        {
            try
            {
                List<BsonDocument> lastSeen = dbinterface.getLastSeen(splitInput[4]).Result;
                List<BsonDocument> logOut = dbinterface.getLogOut(splitInput[4]).Result;

                string answer = ""; 

                if(lastSeen != null)    answer = string.Concat(answer, lastSeen.Last()[4] + " last connected on " + lastSeen.Last()[1] + " at " + lastSeen.Last()[2] + " from " + lastSeen.Last()[3]); 

                if (logOut != null) answer = string.Concat(answer, "\n Disconnected on " + logOut.Last()[1] + " at " + logOut.Last()[2]);
        
                return answer; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null; 
            }
        }

        private List<string> CommandHandler()
        {
            string[] commands = { "!calm", "!rules", "!schedule", "!event" };
            List<string> commandInfo = new List<string>();
            string commandMessage = null; 
            string[] splitCommandMessage;

            foreach(string command in commands)
            {
                if(input.ToLower().Contains(command))
                {
                    commandMessage = input.Substring(input.ToLower().IndexOf(command) + command.Length); 
                    break; 
                }
            }

            splitCommandMessage = commandMessage.Split(new string[] {" , "}, StringSplitOptions.RemoveEmptyEntries); 
            foreach(string command in splitCommandMessage) commandInfo.Add(command);
            Console.WriteLine(commandInfo); 
            return commandInfo; 
        }

        private List<string> JoinParser()
        {
            List<string> joinInfo = new List<string>();

            joinInfo.Add(DateTime.Now.Date.ToString().Substring(0, 10));
            joinInfo.Add(DateTime.Now.TimeOfDay.ToString().Substring(0, 8));
            if (input.Contains("ip.")) joinInfo.Add(input.Substring(input.IndexOf("ip.") + 3, input.IndexOf(" JOIN") - input.IndexOf("ip.") - 3));
            else joinInfo.Add(input.Substring(input.IndexOf("@") + 1, input.IndexOf(" JOIN") - input.IndexOf("@") - 1)); 
            joinInfo.Add(nickname); 

            return joinInfo; 
        }

        private string error()
        {
            string[] error = { "Ara? Something doesn't seem to be right... Try again later.", "Something's wrong honey, try again later, ok?", "That didn't seem to work, can you try again later?", "Darin, that's weird, that didn't seem to work. Canyou try again later?" };
            Random answer = new Random();

            return error[answer.Next(0, error.Length - 1)]; 
        }

        private void GetSender()
        {
            if(splitInput[2] ==  WaifuBot.CHANNEL) sender = WaifuBot.CHANNEL; 

            else sender = GetNick(); 
        }

    }
}
