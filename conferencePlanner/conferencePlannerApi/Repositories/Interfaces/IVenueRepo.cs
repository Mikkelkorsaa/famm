using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IVenueRepo
    {
        /// <summary>
        /// Gets venue object with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Venue Object</returns>
        Task<Venue> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new venue
        /// </summary>
        /// <returns>The venue you just created</returns>
        Task<Venue> CreateAsync(Venue venue);

        /// <summary>
        /// Deletes the venue with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Updates the venue with the given id
        /// </summary>
        /// <param name="venue"></param>
        /// <returns>The updated venue object</returns>
        Task<Venue> UpdateAsync(Venue venue);
    }
}
