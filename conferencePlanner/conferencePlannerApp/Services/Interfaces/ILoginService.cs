using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials</param>
        /// <returns>The logged-in User object</returns>
        Task<User> LoginAsync(LoginModel loginModel);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerModel">The register model containing user details</param>
        Task RegisterAsync(RegisterModel registerModel);
    }
}

