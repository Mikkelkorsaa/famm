using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class LocalVenueService : IVenueService
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


        public Task<Venue> GetByIdAsync(int id)
        {
            var result = _venues.FirstOrDefault(item => item.Id == id);
            if (result != null)
                return Task.FromResult(result);
            else
                throw new Exception("Id doesnt match any existing conferences");
        }

    }
}
