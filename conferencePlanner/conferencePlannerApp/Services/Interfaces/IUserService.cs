using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a list of all users
        /// </summary>
        /// <returns>A list of User objects</returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Updates an existing user in the database
        /// </summary>
        /// <param name="user">The User object to be updated</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Retrieves the current user ID from local storage
        /// </summary>
        /// <returns>The current user ID</returns>
        Task<int?> GetCurrentUserIdAsync();

        /// <summary>
        /// Retrieves a list of users based on a search query and filter criteria
        /// </summary>
        /// <param name="filter">The filter containing search query, length of the list, and number of items to skip</param>
        /// <returns>A list of User objects matching the filter criteria</returns>
        Task<List<User>> GetUsersBySearchOrFilter(UserFilter filter);

        /// <summary>
        /// Retrieves the number of hits for a search query and filter criteria
        /// </summary>
        /// <param name="filter">The filter containing search query, length of the list, and number of items to skip</param>
        /// <returns>The number of hits for the search query</returns>
        Task<int> GetUsersBySearchOrFilterHits(UserFilter filter);

        /// <summary>
        /// Creates a new user and attempts to send it to an API
        /// </summary>
        /// <param name="user">The new User object</param>
        /// <returns>The created User object, with a valid ID if successfully added to the database</returns>
        Task<User> CreateUserAsync(User user);
    }
}

