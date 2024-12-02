using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
  public interface IAuthService
  {
    // Retrieves the current user.
    // Output: A task that represents the asynchronous operation. The task result contains the current User if available; otherwise, null.
    Task<User?> GetCurrentUser();

    // Input: The User to set as the current user.
    // Manipulation: Sets the current user.
    // Output: A task that represents the asynchronous operation.
    Task SetCurrentUser(User user);

    // Clears the current user.
    // Output: A task that represents the asynchronous operation.
    Task ClearCurrentUser();
  }
}
