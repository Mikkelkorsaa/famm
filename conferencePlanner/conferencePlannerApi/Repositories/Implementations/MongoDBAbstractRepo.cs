using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Configurations;
using conferencePlannerCore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace conferencePlannerApi.Repositories.Implementations
{
    public class MongoDBAbstractRepo : IAbstractRepo
    {
        private IMongoCollection<Abstract> _abstractCollection;

        public MongoDBAbstractRepo(IOptions<MongoDBSettings> options)
        {
            var connectionString = options.Value.ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ConferencePlanner");
            _abstractCollection = database.GetCollection<Abstract>("Abstracts");
        }
        public async Task<Abstract> CreateAsync(Abstract abs)
        {
            abs.Id = await GetNextAbstractIdAsync();
            await _abstractCollection.InsertOneAsync(abs);
            return abs;
        }

        public async Task DeleteAsync(int id)
        {
            await _abstractCollection.DeleteOneAsync(abs => abs.Id == id);
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
            var filter = Builders<Abstract>.Filter.Eq(a => a.Id, abstractId);
            var update = Builders<Abstract>.Update.Push(a => a.Reviews, review);
            var result = await _abstractCollection.FindOneAndUpdateAsync(filter, update);
            return result == null ? throw new Exception("Not found") : result;
        }

        public async Task<int> GetNextAbstractIdAsync()
        {
            bool hasValue = await _abstractCollection.AsQueryable().AnyAsync();
            if (!hasValue)
                return 0;

            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id" , 1 },
                    { "MaxAbstractId", new BsonDocument("$max", "$_id") }
                })
            };

            var result = await _abstractCollection
                .Aggregate<BsonDocument>(pipeline)
                .FirstOrDefaultAsync();

            if (result == null)
                return 0;
            return result["MaxAbstractId"].AsInt32 + 1;
        }
    }
}
