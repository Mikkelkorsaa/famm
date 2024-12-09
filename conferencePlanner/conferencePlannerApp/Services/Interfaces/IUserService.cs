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

        //Input a filter object
        //Manipulation: none
        //Output: A list that is will or filter
        Task<List<User>> GetOrFilterUserAsync(UserFilter filter);
  }
}