using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace conferencePlannerApi.Repositories.Implementations
{
    public class MongoDBConferenceRepo : IConferenceRepo
    {
        readonly private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Conference> _conferenceCollection;


        public MongoDBConferenceRepo(IConfiguration config)
        {
            _config = config;
            _mongoClient = new MongoClient(_config["ConnectionStrings:mongoDB"]);
            _database = _mongoClient.GetDatabase("ConferencePlaner");
            _conferenceCollection = _database.GetCollection<Conference>("Conferences");
        }

        public async Task<Conference> CreateAsync(Conference conference)
        {
            conference.Id = await GetNextConferenceIdAsync();
            _conferenceCollection.InsertOne(conference);
            return conference;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", id);
            await _conferenceCollection.DeleteOneAsync(filter);
            return true;
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var filter = Builders<Conference>.Filter.Empty;
            var result = await _conferenceCollection.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task<Conference?> GetByIdAsync(int id)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", id);
            return await _conferenceCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Conference?> UpdateAsync(Conference conference)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", conference.Id);
            await _conferenceCollection.ReplaceOneAsync(filter, conference);
            return await _conferenceCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<int> GetNextConferenceIdAsync()
        {
            var pipeline = new[]
            {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", null },
                { "maxUserId", new BsonDocument("$max", "$_id") }
            })
        };

            var result = await _conferenceCollection
                .Aggregate<BsonDocument>(pipeline)
                .FirstOrDefaultAsync();

            return (result != null ? result["maxUserId"].AsInt32 + 1 : 0) + 1;
        }
    }
}
