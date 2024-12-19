using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    public interface IConferenceRepo
    {
        // Input: Conference ID
        // Output: Conference object or null if not found
        Task<Conference> GetByIdAsync(int id);

        // Output: Collection of all Conference objects
        Task<IEnumerable<Conference>> GetAllAsync();

        /// <summary>
        /// No input is required since the it only needs to know the current date
        /// Return: A List of all Conferences with an EndDate in the future.
        /// </summary>
        Task<IEnumerable<Conference>> GetAllActiveAsync();

		// Input: Conference object without ID
		// Manipulation: Saves to database
		// Output: Conference object with generated ID
		Task<Conference> CreateAsync(Conference conference);

        // Input: Conference ID and updated Conference object
        // Manipulation: Updates existing record
        // Output: Updated Conference object or null if not found
        Task<Conference> UpdateAsync(Conference conference);

        // Input: Conference ID
        // Manipulation: Removes from database
        // Output: True if deleted, false if not found
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Input: ConferenceID
        /// Output: List of all criteria for the conference
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns>List string<returns>

        Task<List<string>> ListAllCriteria(int conferenceId);

        /// <summary>
        /// Input: ConferenceID
        /// Output: List of all categories for the conference
        /// </summary>
        /// <param name="conferenceId"></param>
        /// <returns></returns>
        Task<List<string>> ListAllCategories(int conferenceId);
    }
}