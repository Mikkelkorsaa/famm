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
            _database = _mongoClient.GetDatabase("ConferencePlanner");
            _conferenceCollection = _database.GetCollection<Conference>("Conferences");
        }

        public async Task<Conference> CreateAsync(Conference conference)
        {
            conference.Id = await GetNextConferenceIdAsync();
            Conference response = await _conferenceCollection.Find(Builders<Conference>.Filter.Eq("_id", conference.Id)).FirstOrDefaultAsync();
            if (response == null)
            {
                _conferenceCollection.InsertOne(conference);
                return conference;
            }
            else
                throw new Exception("Conference ID already exists");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", id);
            var result = await _conferenceCollection.DeleteOneAsync(filter);
            return (result.DeletedCount == 0) ? throw new Exception("Conference not found") : true;
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var filter = Builders<Conference>.Filter.Empty;
            var response = await _conferenceCollection.FindAsync(filter);
            var result = response.ToListAsync();
            return (result != null) ? result.Result : throw new Exception("No conferences found");
        }

        public async Task<Conference> GetByIdAsync(int id)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", id);
            var response = await _conferenceCollection.Find(filter).FirstOrDefaultAsync();
            return (response != null) ? response : throw new Exception("Conference not found");
        }

        public async Task<Conference> UpdateAsync(Conference conference)
        {
            var filter = Builders<Conference>.Filter.Eq("_id", conference.Id);
            var response = await _conferenceCollection.ReplaceOneAsync(filter, conference);
            var updatedConference = await _conferenceCollection.Find(filter).FirstOrDefaultAsync();
            return (response.ModifiedCount == 0) ? throw new Exception("Conference not found") : updatedConference;
        }

        // Help function, gets the highest ID, and adds 1, ensuring that we have an unused valid ID.
        // If the collection is empty it will be assigned the value 0.
        public async Task<int> GetNextConferenceIdAsync()
        {
            var pipeline = new[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", 1 },
                    { "maxUserId", new BsonDocument("$max", "$_id") }
                })
            };

            var result = await _conferenceCollection
                .Aggregate<BsonDocument>(pipeline)
                .FirstOrDefaultAsync();

            return (result != null ? result["maxUserId"].AsInt32 + 1 : 0) + 1;
        }

        public Task<List<string>> ListAllCriteria(Conference conference)
        {
            throw new NotImplementedException();
        }
    }
}