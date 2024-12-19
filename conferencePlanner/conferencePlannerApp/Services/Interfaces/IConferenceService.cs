using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IConferenceService
    {
        /// <summary>
        /// Creates a new conference and stores it in the database
        /// </summary>
        /// <param name="conference">The Conference object to be created</param>
        /// <returns>The created Conference object</returns>
        Task<Conference> CreateConferenceAsync(Conference conference);

        /// <summary>
        /// Retrieves all active conferences that have not yet concluded
        /// </summary>
        /// <returns>A List of active Conference objects</returns>
        Task<List<Conference>> GetActiveConferencesAsync();

        /// <summary>
        /// Retrieves all conferences
        /// </summary>
        /// <returns>A List of Conference objects</returns>
        Task<List<Conference>> GetConferencesAsync();

        /// <summary>
        /// Retrieves a conference by its ID
        /// </summary>
        /// <param name="id">The ID of the conference</param>
        /// <returns>The Conference object</returns>
        Task<Conference> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing conference in the database
        /// </summary>
        /// <param name="conference">The Conference object to be updated</param>
        /// <returns>The updated Conference object</returns>
        Task<Conference> UpdateAsync(Conference conference);

        /// <summary>
        /// Retrieves the current conference ID from local storage
        /// </summary>
        /// <returns>The current conference ID</returns>
        Task<int> GetCurrentConferenceIdAsync();

        /// <summary>
        /// Sets the current conference ID in local storage
        /// </summary>
        /// <param name="id">The ID of the conference</param>
        Task SetCurrentConferenceAsync(int id);

        /// <summary>
        /// Retrieves the review criteria for a specific conference by its ID
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <returns>A list of strings</returns>
        Task<List<string>> GetCriteriaByIdAsync(int conferenceId);

        /// <summary>
        /// Retrieves the categories associated with a specific conference by its ID
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <returns>A list of strings</returns>
        Task<List<string>> GetCategoriesByIdAsync(int conferenceId);
    }
}


