
using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Configurations;
using conferencePlannerCore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBVenueRepo : IVenueRepo
{
	private readonly IMongoCollection<Venue> _collection;

	public MongoDBVenueRepo(IOptions<MongoDBSettings> options)
	{
		var connectionString = options.Value.ConnectionString;
		var client = new MongoClient(connectionString);
		var database = client.GetDatabase("ConferencePlanner");
		_collection = database.GetCollection<Venue>("Venues");
	}
	public async Task<Venue> CreateAsync(Venue venue)
	{
		venue.Id = await GetNextUserIdAsync();
		var response = await _collection.Find(Builders<Venue>.Filter.Eq("Address", venue.Address)).FirstOrDefaultAsync();
		if (response == null)
		{
			await _collection.InsertOneAsync(venue);
			return venue;
		}
		else
		{
			throw new Exception("Address already in use");
		}
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var filter = Builders<Venue>.Filter.Eq("_id", id);
		var result = await _collection.DeleteOneAsync(filter);
		return (result.DeletedCount == 0) ? throw new Exception("Venue not found") : true;
	}

	public async Task<Venue> GetByIdAsync(int id)
	{
		var filter = Builders<Venue>.Filter.Eq("_id", id);
		var response = await _collection.Find(filter).FirstOrDefaultAsync();
		return (response != null) ? response : throw new Exception("Venue not found");
	}

	public async Task<Venue> UpdateAsync(Venue venue)
	{
		var filter = Builders<Venue>.Filter.Eq("_id", venue.Id);
		var response = await _collection.ReplaceOneAsync(filter, venue);
		var updatedVenue = await _collection.Find(filter).FirstOrDefaultAsync();
		return (response.ModifiedCount == 0) ? throw new Exception("Venue not found") : updatedVenue;
	}

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

		var result = await _collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

		if (result == null)
			return 0;
		return result["maxUserId"].AsInt32 + 1;
	}

}

