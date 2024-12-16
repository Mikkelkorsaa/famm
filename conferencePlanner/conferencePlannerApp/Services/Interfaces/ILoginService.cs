using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
  public interface ILoginService
  {
    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns>User object</returns>
    Task<User> LoginAsync(LoginModel loginModel);
    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="registerModel"></param>''
    Task RegisterAsync(RegisterModel registerModel);
  }
}