﻿using conferencePlannerCore.Models;
using conferencePlannerApi.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace conferencePlannerApi.Repositories.Implementations
{
    public class MongoDBUserRepo : IUserRepo
    {
        readonly private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<User> _userCollection;


        public MongoDBUserRepo(IConfiguration config)
        {
            _config = config;
            _mongoClient = new MongoClient(_config["ConnectionStrings:mongoDB"]);
            _database = _mongoClient.GetDatabase("ConferencePlaner");
            _userCollection = _database.GetCollection<User>("Users");
        }

        public async Task<User?> CreateAsync(User user)
        {
            User? response = await _userCollection.Find(Builders<User>.Filter.Eq("Email", user.Email)).FirstOrDefaultAsync();
            if (response == null)
            {
                user.Id = await GetNextUserIdAsync();
                _userCollection.InsertOne(user);
                return user;
            }
            else
                throw new Exception("email in use");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            await _userCollection.DeleteOneAsync(filter);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var filter = Builders<User>.Filter.Empty;
            var result = await _userCollection.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();           
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            await _userCollection.ReplaceOneAsync(filter, user);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }


        // Help function, gets the highest ID, and adds 1, insuring that we have a unused valid ID.
        // If the collection is empty it will be be asigned the value 0.  
        public async Task<int> GetNextUserIdAsync()
        {
            var pipeline = new[]
            {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", null },
                { "maxUserId", new BsonDocument("$max", "$_id") }
            })
        };

            var result = await _userCollection
                .Aggregate<BsonDocument>(pipeline)
                .FirstOrDefaultAsync();

            return (result != null ? result["maxUserId"].AsInt32 +1 : 0) + 1;
        }
    }
}
