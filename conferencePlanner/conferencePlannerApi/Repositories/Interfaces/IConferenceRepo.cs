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

    //Input: Conference object
    //Output: List of all criteria for the conference
    Task<List<string>> ListAllCriteria(Conference conference);
  }
}