using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerApi.MongoResponseClasses;
using conferencePlannerCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using conferencePlannerCore.Configurations;

namespace conferencePlannerApi.Repositories.Implementations
{
    public class MongoDBConferenceRepo : IConferenceRepo
    {
        private IMongoCollection<Conference> _conferenceCollection;

        public MongoDBConferenceRepo(IOptions<MongoDBSettings> options)
        {
            var connectionString = options.Value.ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ConferencePlanner");
            _conferenceCollection = database.GetCollection<Conference>("Conferences");
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

		public async Task<IEnumerable<Conference>> GetAllActiveAsync()
		{
			var filter = Builders<Conference>.Filter.Gt("EndDate", DateTime.Now);
			var response = await _conferenceCollection.FindAsync(filter);
			var result = response.ToListAsync();
			return (result != null) ? result.Result : throw new Exception("No conferences found");
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

            return (result != null ? result["maxUserId"].AsInt32 + 1 : 0);
        }

        public async Task<List<string>> ListAllCriteria(int conferenceId)
        {
            var filter = Builders<Conference>
                .Filter.Eq("_id", conferenceId);

            var projection = Builders<Conference>
                .Projection.Include("ReviewCriteria")
                .Exclude("_id");

            var result = await _conferenceCollection
                .Find(filter)
                .Project<ConferenceCriteria>(projection)
                .FirstOrDefaultAsync();

            return result == null ? new List<string>() : result.ReviewCriteria ;
        }

        public async Task<List<string>> ListAllCategories(int conferenceId)
        {
            var filter = Builders<Conference>
                .Filter.Eq("_id", conferenceId);

            var projection = Builders<Conference>
                .Projection.Include("Category")
                .Exclude("_id");

            var result = await _conferenceCollection
                .Find(filter)
                .Project<ConferenceCategories>(projection)
                .FirstOrDefaultAsync();

            return result.Category ?? new List<string>();
        }
    }
}