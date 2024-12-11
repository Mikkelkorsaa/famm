﻿using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace conferencePlannerApi.Repositories.Implementations
{
    public class MongoDBAbstractRepo : IAbstractRepo
    {
        readonly private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Abstract> _abstractCollection;

        public MongoDBAbstractRepo(IConfiguration config)
        {
            _config = config;
            _mongoClient = new MongoClient(_config["ConnectionStrings:mongoDB"]);
            _database = _mongoClient.GetDatabase("ConferencePlaner");
            _abstractCollection = _database.GetCollection<Abstract>("abstracts");
        }
        public async Task<Abstract?> CreateAsync(Abstract abs)
        {
            Abstract absWithId = abs;
            absWithId.Id = await GetNextAbstractIdAsync();
            await _abstractCollection.InsertOneAsync(absWithId);
            return absWithId;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _abstractCollection.DeleteOneAsync(abs => abs.Id == id);
            Abstract result = await GetByIdAsync(id);
            return result == null;
        }

        public async Task<List<Abstract>> GetAllAsync()
        {
            var filter = Builders<Abstract>.Filter.Empty;
            var result = await _abstractCollection.FindAsync(filter);
            return result == null ? throw new Exception("there were non") : result.ToList();
        }

        public async Task<Abstract> GetByIdAsync(int id)
        {
            var filter = Builders<Abstract>.Filter.Eq(a => a.Id, id);
            var result = await _abstractCollection.Find(filter).FirstOrDefaultAsync();
            return result==null ? throw new Exception("not found") : result;
        }

        public async Task<Abstract> UpdateAsync(Abstract abs)
        {
            var filter = Builders<Abstract>.Filter.Eq(a => a.Id, abs.Id);
            _abstractCollection.ReplaceOne(filter, abs);
            var result = await _abstractCollection.Find(filter).FirstOrDefaultAsync();
            return result == null ? throw new Exception("Not found") : result; 
            
        }

        public async Task<Abstract> UpdateReview(int abstractId, Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextAbstractIdAsync()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", null },
                    { "MaxValue", new BsonDocument("$max", "$_id") }
                })
            };

            var result = await _abstractCollection
                .Aggregate<BsonDocument>(pipeline)
                .FirstOrDefaultAsync();

            return (result != null ? result["MaxValue"].AsInt32 + 1 : 0) + 1;
        }
    }
}
