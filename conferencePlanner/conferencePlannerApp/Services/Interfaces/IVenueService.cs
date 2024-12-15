using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces;

public interface IVenueService
{
    /// <summary>
    /// Return a VenueObject with the given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>VenueObject</returns>
    Task<Venue> GetByIdAsync(int id);
}

