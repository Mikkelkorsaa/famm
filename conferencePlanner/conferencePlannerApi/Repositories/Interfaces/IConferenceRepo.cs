using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IConferenceRepo
    {
        /// <summary>
        /// Retrieves a Conference by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Conference.</param>
        /// <returns>The Conference object if found</returns>
        Task<Conference> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all Conference entities from the database.
        /// </summary>
        /// <returns>A collection containing all Conference objects.</returns>
        Task<IEnumerable<Conference>> GetAllAsync();

        /// <summary>
        /// No input is required since the it only needs to know the current date
        /// Return: A List of all Conferences with an EndDate in the future.
        /// </summary>
        /// <returns>A collection containing all active Conferences</returns>
        Task<IEnumerable<Conference>> GetAllActiveAsync();

        /// <summary>
        /// Creates a new Conference entity in the database.
        /// </summary>
        /// <param name="conference">The Conference object to create (without ID).</param>
        /// <returns>The created Conference object</returns>
        Task<Conference> CreateAsync(Conference conference);

        /// <summary>
        /// Updates an existing Conference entity in the database.
        /// </summary>
        /// <param name="conference">The Conference object with updated values.</param>
        /// <returns>The updated Conference object if found</returns>
        Task<Conference> UpdateAsync(Conference conference);

        /// <summary>
        /// Deletes a Conference entity from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the Conference to delete.</param>
        /// <returns>True if the Conference was deleted; false if not found.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Input: ConferenceID
        /// Output: List of all criteria for the conference
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns>A collection of strings</returns>
        Task<List<string>> ListAllCriteria(int conferenceId);

        /// <summary>
        /// Input: ConferenceID
        /// Output: List of all categories for the conference
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns>A collection of strings</returns>
        Task<List<string>> ListAllCategories(int conferenceId);
    }
}