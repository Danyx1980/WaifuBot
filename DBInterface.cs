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
        protected static IMongoDatabase eventDatabase = client.GetDatabase("Events");
        protected static IMongoDatabase logInDatabase = client.GetDatabase("LogIn");
        protected static IMongoDatabase userDatabase = client.GetDatabase("Users"); 

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
                var collection = eventDatabase.GetCollection<BsonDocument>("Events");
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

                var collection = eventDatabase.GetCollection<BsonDocument>("Events");
                await collection.InsertOneAsync(toInsert);
            }
        }

        public async Task<List<BsonDocument>> getEvent(string eventInfo)
        {
            var collection = eventDatabase.GetCollection<BsonDocument>("Events");
            var filter = Builders<BsonDocument>.Filter.Eq("Event", eventInfo);
            List<BsonDocument> result = await collection.Find(filter).ToListAsync(); 

            return result.ToList();
        }
    }
}
