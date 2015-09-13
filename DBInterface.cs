using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WaifuBot
{
    class DBInterface
    {

        protected static IMongoClient client = new MongoClient();
        protected static IMongoDatabase waifuBotDatabase = client.GetDatabase("Events");

        protected static string eventCollection = "Events";
        protected static string logInCollection = "LogIn";
        protected static string userCollection = "Users"; 

        public async void insertEvent(List<string> eventInfo)
        {
            if (eventInfo.Count == 4)
            {
                var toInsert = new BsonDocument
            {
                {"Event", eventInfo[0]}, 
                {"Date", eventInfo[1]}, 
                {"Hour", eventInfo[2]}, 
                {"Extra", eventInfo[3]}
            };
                var collection = waifuBotDatabase.GetCollection<BsonDocument>(eventCollection);
                await collection.InsertOneAsync(toInsert); 
            }

            if (eventInfo.Count == 3)
            {
                var toInsert = new BsonDocument
            {
                {"Event", eventInfo[0]}, 
                {"Date", eventInfo[1]}, 
                {"Hour", eventInfo[2]}
            };

                var collection = waifuBotDatabase.GetCollection<BsonDocument>(eventCollection);
                await collection.InsertOneAsync(toInsert);
            }
        }

        public async Task<List<BsonDocument>> getEvent(string eventInfo)
        {
            var collection = waifuBotDatabase.GetCollection<BsonDocument>(eventCollection);
            var filter = Builders<BsonDocument>.Filter.Eq("Event", eventInfo);
            List<BsonDocument> result = await collection.Find(filter).ToListAsync(); 

            return result.ToList();
        }

        public async void inserLogIn(List<string> LogInfo)
        {
            var toInsert = new BsonDocument
            {
                {"Date", LogInfo[0]},
                {"Hour", LogInfo[1]}, 
                {"IP", LogInfo[2]}, 
                {"User", LogInfo[3]},
                {"ID", LogInfo[3].ToLower()}
            };

            var collection = waifuBotDatabase.GetCollection<BsonDocument>(logInCollection);
            await collection.InsertOneAsync(toInsert); 
        }

        public async Task<List<BsonDocument>> getLastSeen(string user)
        {
            var collection = waifuBotDatabase.GetCollection<BsonDocument>(logInCollection);
            var filter = Builders<BsonDocument>.Filter.Eq("ID", user.ToLower());
            List<BsonDocument> result = await collection.Find(filter).ToListAsync();

            return result.ToList(); 
        }
    }
}
