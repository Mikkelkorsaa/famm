using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using System.Net.Http.Json;
using Blazored.LocalStorage;
namespace conferencePlannerApp.Services.LocalImplementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string IdStorageKey = "currentUserId";

        public UserService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
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

        public async Task<int?> GetCurrentUserIdAsync()
        {
            
            var userId = await _localStorage.GetItemAsync<int?>(IdStorageKey);
            
            if (userId == 0)
                return 0;
            else
                return userId;
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