using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
  public interface IAuthService
  {
    Task<User?> GetCurrentUser();
    Task SetCurrentUser(User user);
    Task ClearCurrentUser();
  }
}
