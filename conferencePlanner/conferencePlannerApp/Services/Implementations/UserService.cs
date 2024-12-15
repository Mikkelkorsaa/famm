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

        public Task<int?> GetCurrentUserIdAsync()
        {
            throw new NotImplementedException();
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