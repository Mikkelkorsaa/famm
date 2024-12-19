using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IVenueRepo
    {
        /// <summary>
        /// Retrieves a venue by its id
        /// </summary>
        /// <param name="id">The id of the venue to retrieve</param>
        /// <returns>A Venue object if found</returns>
        Task<Venue> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new venue in the database
        /// </summary>
        /// <param name="venue">The Venue object to create </param>
        /// <returns>The created Venue object </returns>
        Task<Venue> CreateAsync(Venue venue);

        /// <summary>
        /// Deletes a venue from the database matching the given id
        /// </summary>
        /// <param name="id">The id of the venue to delete</param>
        /// <returns>True if the venue was deleted</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Updates an existing venue in the database
        /// </summary>
        /// <param name="venue">The Venue object containing updated information</param>
        /// <returns>The updated Venue object if found</returns>
        Task<Venue> UpdateAsync(Venue venue);
    }
}
