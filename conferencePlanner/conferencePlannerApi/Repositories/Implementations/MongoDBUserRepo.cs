using conferencePlannerCore.Models;
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
            _database = _mongoClient.GetDatabase("ConferencePlanner");
            _userCollection = _database.GetCollection<User>("Users");
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = await GetNextUserIdAsync();
            User response = await _userCollection.Find(Builders<User>.Filter.Eq("Email", user.Email)).FirstOrDefaultAsync();
            if (response == null)
            {
                _userCollection.InsertOne(user);
                return user;
            }
            else
                throw new Exception("Email already in use");
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var result = await _userCollection.DeleteOneAsync(filter);
            return (result.DeletedCount == 0) ? throw new Exception("User not found") : true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var filter = Builders<User>.Filter.Empty;
            var response = await _userCollection.FindAsync(filter);
            var result = response.ToListAsync();
            return (result != null) ? result.Result : throw new Exception("No users found");
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            var response = await _userCollection.Find(filter).FirstOrDefaultAsync();
            return (response != null) ? response : throw new Exception("User not found");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var response = await _userCollection.Find(filter).FirstOrDefaultAsync();
            return (response != null) ? response : throw new Exception("User not found");
        }

        public async Task<User> UpdateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            var response = await _userCollection.ReplaceOneAsync(filter, user);
            var updatedUser = await _userCollection.Find(filter).FirstOrDefaultAsync();
            return (response.ModifiedCount == 0) ? throw new Exception("User not found") : updatedUser;
        }


        // Help function, gets the highest ID, and adds 1, insuring that we have a unused valid ID.
        // If the collection is empty it will be be asigned the value 0.  
        public async Task<int> GetNextUserIdAsync()
        {
            var pipeline = new[]
            {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", 1 },
                { "maxUserId", new BsonDocument("$max", "$_id") }
            })
        };

            var result = await _userCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

            if (result == null)
                return 0;
            return result["maxUserId"].AsInt32 + 1;
        }
    }
}
