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
    Task<User> CreateUserAsync(User user);
  }
}