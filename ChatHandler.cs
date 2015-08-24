﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuBot
{
    class ChatHandler
    {
        public int poiCounter = 0;
        public string nickname;

        public string Response(string message)
        {
            string[] input = message.Split(new Char[] { ' ' });
            if (input[1] != "NICK")
            {
                nickname = GetNick(input[0]);

                if (input[1] == "JOIN" && input[0].Contains("WaifuBot")) return SelfJoin();
                if (input[input.Length - 2] == "JOIN") return UserJoin();
                if (IsHentai(input)) return Hentai();
                if (IsHello(message)) return Domo();
                if (IsMean(message)) return Sad();
                if (IsLove(message)) return Love();
                if (IsCalm(message)) return Calm();
                if (IsAyy(input)) return Lmao();
                if (IsRules(message)) return Rules(); 
                //if (IsPoi(input)) return poiCounter.ToString(); 
            }
            return null;
        }

        private string SelfJoin()
        {
            string[] selfJoinResponse = { "Ara?", "Tadaima", "I'm back!", "Darin! I'm back!" };
            Random answer = new Random();

            //return selfJoinResponse[answer.Next(0, selfJoinResponse.Length)];
            return selfJoinResponse[answer.Next(0, selfJoinResponse.Length)];
        }

        private string UserJoin()
        {
            string[] userJoinResponse = { "Ara? Welcome back!", "Okaerinasai", "Okaeri", "Welcome back darin!" };
            Random answer = new Random();

            if (answer.Next(0, 100) == 37) return "Welcome back darin! Would you want dinner? A bath? Or perhaps \r\n Me?";

            return userJoinResponse[answer.Next(0, userJoinResponse.Length)];
        }

        private string Hentai()
        {
            string[] hentaiResponse = { "L-lewd!", "Uh- UUHH?!", "D-darin, you like those stuff?", "W-What are you talking about?!" };
            Random answer = new Random();

            return hentaiResponse[answer.Next(0, hentaiResponse.Length)];
        }

        private bool IsPoi(string[] input)
        {
            foreach (string substring in input)
            {
                if (substring.ToLower().Contains("poi"))
                {
                    poiCounter++;
                    return true;
                }
            }

            return false;
        }

        private bool IsHentai(string[] input)
        {
            foreach (string substring in input)
            {
                if (substring.ToLower().Contains("hentai")) return true;
            }

            return false;
        }

        private bool IsHello(string input)
        {
            string[] possibleSalutes = { "hello", "kombawa", "ohayo", "good morning", "morning", "hey", "ohayou" }; 
            if (input.ToLower().Contains("domo")) return true;
            
            foreach(string salutes in possibleSalutes)
            {
                if (input.ToLower().Contains(salutes) && input.ToLower().Contains("waifubot")) return true; 
            }

            return false; 
        }

        private bool IsMean(string input)
        {
            string[] possibleHate = { "fuck you", "i hate you", "you suck", "youre useless", "you're useless", "you useless", "nobody wants you", "get out", "fuck off" };

            foreach (string hate in possibleHate)
            {
                if (input.ToLower().Contains(hate) && input.ToLower().Contains("waifubot")) return true;
            }

            return false; 
        }

        private bool IsLove(string input)
        {
            string[] possibleLove = { "i love you", "you're so cool", "you so cool", "youre so cool", "you're awesome", "you awesome", "youre awesome", "is cool", "is cute", "is pretty", "are cute", "are pretty", "you're pretty", "you're so pretty", "youre pretty", "youre so pretty", "youre cute", "youre so cute", "you are so pretty", "you are so cute", "you are so cool", "i love", "is amazing", "is awesome", "you are awesome" };

            foreach (string love in possibleLove)
            {
                if (input.ToLower().Contains(love)) return true;
            }

            return false; 
        }

        private bool IsCalm(string input)
        {
            if (input.ToLower().Contains("!calm")) 
            {
                string substring = input.Substring(input.IndexOf("!calm"));
                string[] substringsplit = substring.Split(new Char[] { ' ' });
                    nickname = substringsplit[1];
                    return true;
            }

            return false; 
        }

        private bool IsAyy(string[] input)
        {
            if (input[3].ToLower().Contains("ayy") && input.Length == 4) return true;

            return false; 
        }

        private bool IsRules(string input)
        {
            if (input.ToLower().Contains("!rules")) return true;

            return false; 
        }

        private string Domo()
        {
            string[] domoResponse = { "Ara? Ohayou darlin ", "Good morning ", "Hmmm! I don't like being called that! you know? " };
            Random answer = new Random();

            if (nickname.Contains("Cult_films") && answer.Next(0, 20) == 13) return "Uh? Don't get near me, you creep";
            if (nickname.ToLower().Contains("anpan")) return (domoResponse[answer.Next(0, domoResponse.Length - 1)] + nickname);
            return (domoResponse[answer.Next(0, domoResponse.Length - 1)] + nickname + "-san");
        }

        private string Sad()
        {
            string[] hateResponses = { "Wow so mean, ", "/me cries \r\n Why so mean ", "I hve feelings too, you know? " };
            Random answer = new Random();

            if (nickname.ToLower().Contains("mahdi") && answer.Next(0, 10) == 5) return "No need to be so mean, Mahdi-san"; 
            if (nickname.Contains("Rumi") && answer.Next(0, 10) == 3) return "Wow Rumi-chan, you're so mean :("; 
            if(answer.Next(0, 999) == 1337) return ("It's ok, I still love you " + nickname + "-san");

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
            string[] rules = { "Post dongers.\n1. Praise Camilla. \n2. Praise Anna. \n3. Love Saki. \n4. Never talk about 9/7." };
            return rules[0]; 
        }

        private string GetNick(string input)
        {

            int index = input.IndexOf("!");

            if (index >= 0) return input.Substring(1, index - 1);

            return null;
        }

    }
}
