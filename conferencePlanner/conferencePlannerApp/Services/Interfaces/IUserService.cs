using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IUserService
    {
        // Output: List of all users
        Task<List<User>> GetAllUsersAsync();

        // Input: User object
        // Manipulation: Saves to database
        // Output: User object with generated ID
        Task UpdateUserAsync(User user);
        /// <summary>
        /// Get the current userId in local storage
        /// </summary>

        /// <returns>UserId</returns>
        Task<int?> GetCurrentUserIdAsync();
        /// <summary>
        /// Input: A filter containing, a search query string, int length of the list called shown and a int how many should be skipped, 
        /// so you chose the maximum amount shown.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>A list of Users, length can be set by the filter</returns>
        Task<List<User>> GetUsersBySearchOrFilter(UserFilter filter);
        /// <summary>
        /// A filter containing, a search query string, int length of the list called shown and a int how many should be skipped, 
        /// so you chose the maximum amount shown.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>how many hits does this search have</returns>
        Task<int> GetUsersBySearchOrFilterHits(UserFilter filter);


    }
}