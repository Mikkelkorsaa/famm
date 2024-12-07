using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class LocalUserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public LocalUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7000/api/user/getallusers");
            response.EnsureSuccessStatusCode();
            var users = await response.Content.ReadFromJsonAsync<List<User>>();
            if (users == null)
            {
                throw new InvalidOperationException("Failed to retrieve users.");
            }
            return users;
        }

        public async Task UpdateUserAsync(User user)
        {
            var users = await GetAllUsersAsync();

            var index = users.FindIndex(u => u.Id == user.Id);
            if (index == -1)
            {
                throw new ArgumentException("User not found");
            }
            users[index] = user;

            var response = await _httpClient.PutAsJsonAsync("https://localhost:7000/api/user/updateuser", user);
            response.EnsureSuccessStatusCode();
            var updatedUsers = await GetAllUsersAsync();
        }
    }

}