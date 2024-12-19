using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
    // Repository interface for User entity operations
    public interface IUserRepo
    {
        /// <summary>
        /// Retrieves a user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>A User object if found</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        /// <returns>A collection of all User objects</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Creates a new user in the database
        /// </summary>
        /// <param name="user">The User object to create</param>
        /// <returns>The created User object</returns>
        Task<User> CreateAsync(User user);

        /// <summary>
        /// Updates an existing user in the database
        /// </summary>
        /// <param name="user">The User object containing updated information</param>
        /// <returns>The updated User object if found</returns>
        Task<User> UpdateAsync(User user);

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <returns>True if the user was deleted</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves a user by their email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>A User object if found</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Searches and filters users based on provided criteria
        /// </summary>
        /// <param name="filter">A UserFilter object</param>
        /// <returns>A list of users matching the filter criteria</returns>
        Task<List<User>> GetFilterORSearch(UserFilter filter);
        /// <summary>
        /// Counts the number of users matching a search query
        /// </summary>
        /// <param name="filter">A UserFilter object (only Query property is used)</param>
        /// <returns>The total number of users matching the search criteria</returns>
        Task<int> GetFilterOrSearchNumberOfHits(UserFilter filter);
    }
}