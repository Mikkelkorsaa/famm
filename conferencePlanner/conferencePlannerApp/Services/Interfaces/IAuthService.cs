using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
  public interface IAuthService
  {
    // Output: Current user or null if not logged in
    Task<User> GetCurrentUser();

    // Input: User object
    // Manipulation: Saves to local storage
    Task SetCurrentUser(User user);

    // Manipulation: Removes user from local storage
    Task ClearCurrentUser();
  }
}
