using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Retrieves the current logged-in user
        /// </summary>
        /// <returns>The current User object or null if not logged in</returns>
        Task<User> GetCurrentUser();

        /// <summary>
        /// Sets the current user and saves it to local storage
        /// </summary>
        /// <param name="user">The User object to be set as current user</param>
        Task SetCurrentUser(User user);

        /// <summary>
        /// Sets the current user ID
        /// </summary>
        /// <param name="user">The User object whose ID is to be set</param>
        Task SetCurrentUserId(User user);

        /// <summary>
        /// Clears the current user from local storage
        /// </summary>
        Task ClearCurrentUser();
    }
}

