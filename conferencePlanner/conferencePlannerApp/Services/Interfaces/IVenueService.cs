using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces;

public interface IVenueService
{
    /// <summary>
    /// Retrieves a Venue object by its ID
    /// </summary>
    /// <param name="id">The ID of the venue</param>
    /// <returns>The Venue object</returns>
    Task<Venue> GetByIdAsync(int id);
}

