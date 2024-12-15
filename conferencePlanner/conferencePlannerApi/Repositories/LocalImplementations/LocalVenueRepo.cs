using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.LocalImplementations;

public class LocalVenueRepo : IVenueRepo
{
    private readonly List<Venue> _venues = new()
    {
        new Venue
            {
                Id = 1,
                Name = "Grand Convention Center",
                Address = "123 Main Street, Cityville",
                Rooms = new List<Room>
                {
                    new Room { Id = 1, Name = "Main Hall" },
                    new Room { Id = 2, Name = "Conference Room A" },
                    new Room { Id = 3, Name = "Conference Room B" }
                }
            },
            new Venue
            {
                Id = 2,
                Name = "Downtown Event Space",
                Address = "456 Elm Street, Townsville",
                Rooms = new List<Room>
                {
                    new Room { Id = 4, Name = "Auditorium" },
                    new Room { Id = 5, Name = "Meeting Room 1" },
                    new Room { Id = 6, Name = "Meeting Room 2" }
                }
            },
            new Venue
            {
                Id = 3,
                Name = "Riverside Banquet Hall",
                Address = "789 River Road, Villageburg",
                Rooms = new List<Room>
                {
                    new Room { Id = 7, Name = "Banquet Hall" },
                    new Room { Id = 8, Name = "Private Dining Room" }
                }
            }
    };
    private int newId() => _venues.Max(v => v.Id);

    public async Task<Venue> CreateAsync(Venue venue)
    {
        var response = _venues.FirstOrDefault(v => v.Id == venue.Id);
        if (response == null)
        {
            int latestId = newId();
            var newVenue = venue with { Id = ++latestId };
            _venues.Add(newVenue);
            return await Task.FromResult(newVenue);
        }
        else
            throw new Exception("Venue ID already exists");
    }

    public async Task<bool> DeleteVenueAsync(int id)
    {
        var existing = _venues.FirstOrDefault(v => v.Id == id);
        if (existing == null)
            throw new Exception("Venue not found");

        _venues.Remove(existing);
        return await Task.FromResult(true);
    }

    public async Task<Venue> GetByIdAsync(int id)
    {
        var response = _venues.FirstOrDefault(v => v.Id == id);
        return await Task.FromResult(response != null ? response : throw new Exception("Venue not found"));
    }
}

