using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using System.Net.Http.Json;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            if (newUser == null)
                throw new Exception("User is null");
            var response = await _httpClient.PostAsJsonAsync("/api/abstract/CreateUser", newUser); ;
            response.EnsureSuccessStatusCode();
            var responseUser = await response.Content.ReadFromJsonAsync<User>();
            return responseUser != null ? responseUser : throw new Exception("User == null");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/user/getallusers");
                response.EnsureSuccessStatusCode();

                var users = await response.Content.ReadFromJsonAsync<List<User>>()
                    ?? throw new InvalidOperationException("Failed to deserialize users response.");

                return users;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to fetch users from the API.", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/user/updateuser", user);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update user with ID {user.Id}.", ex);
            }
        }
    }

}