using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
  public interface IAuthService
  {
    //Input a login model, with password and username
    //Output a user if login is succesfull.
    Task<(User?, string)> Login(LoginModel loginModel); 
    
    // Output: Current user or null if not logged in
    Task<User?> GetCurrentUser();

    // Input: User object
    // Manipulation: Saves to local storage
    Task SetCurrentUser(User user);

    // Manipulation: Removes user from local storage
    Task Logout();
    
    //Ínput a user object,
    //ID will be assigned by the repo
    //Output, an accepted user or null if the Email is taken.
    Task<(User?, string)> CreateUser(User user);
  }
}
