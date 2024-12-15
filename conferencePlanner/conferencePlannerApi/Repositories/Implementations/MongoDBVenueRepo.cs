using conferencePlannerCore.Models;
using conferencePlannerApi.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace conferencePlannerApi.Repositories.Implementations;

public class MongoDBVenueRepo : IVenueRepo
{
    readonly private IConfiguration _config;
    private IMongoClient _mongoClient;
    private IMongoDatabase _database;
    private IMongoCollection<Venue> _venueCollection;

    public MongoDBVenueRepo(IConfiguration config)
    {
        _config = config;
        _mongoClient = new MongoClient(_config["ConnectionStrings:mongoDB"]);
        _database = _mongoClient.GetDatabase("ConferencePlaner");
        _venueCollection = _database.GetCollection<Venue>("Venues");
    }

    public async Task<Venue> CreateAsync(Venue venue)
    {
        venue.Id = await GetNextVenueIdAsync();
        var response = await _venueCollection.Find(Builders<Venue>.Filter.Eq("_id", venue.Id)).FirstOrDefaultAsync();
        if (response == null)
        {
            await _venueCollection.InsertOneAsync(venue);
            return venue;
        }
        else
            throw new Exception("Venue ID already exists");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var filter = Builders<Venue>.Filter.Eq("_id", id);
        var result = await _venueCollection.DeleteOneAsync(filter);
        return (result.DeletedCount == 0) ? throw new Exception("Venue not found") : true;
    }

    public async Task<Venue> GetByIdAsync(int id)
    {
        var filter = Builders<Venue>.Filter.Eq("_id", id);
        var response = await _venueCollection.Find(filter).FirstOrDefaultAsync();
        return (response != null) ? response : throw new Exception("Venue not found");
    }

    public async Task<Venue> UpdateAsync(Venue venue)
    {
        var filter = Builders<Venue>.Filter.Eq("_id", venue.Id);
        var response = await _venueCollection.ReplaceOneAsync(filter, venue);
        var updatedVenue = await _venueCollection.Find(filter).FirstOrDefaultAsync();
        return (response.ModifiedCount == 0) ? throw new Exception("Venue not found") : updatedVenue;
    }

    private async Task<int> GetNextVenueIdAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", null },
                { "maxVenueId", new BsonDocument("$max", "$_id") }
            })
        };

        var result = await _venueCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
        return result == null ? 1 : result["maxVenueId"].AsInt32 + 1;
    }
}