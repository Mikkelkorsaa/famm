using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace conferencePlannerApp.Services.Implementations
{
    public class AuthServiceImpl : IAuthService
    {
        public HttpClient _httpClient { get; set; }
        public ILocalStorageService _localStorage { get; set; }

        AuthServiceImpl(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task Logout()
        {
            await _localStorage.SetItemAsync<User?>("user", null);
        }

        public async Task<User?> GetCurrentUser()
        {
            return await _localStorage.GetItemAsync<User>("user"); 
        }

        public async Task<(User?, string)> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7000/api/user/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    await SetCurrentUser(user);
                    return (user, string.Empty);
                }
                else
                {
                    return (null, "Error retrieving user information");
                }
            }
            else
            {
                return (null, "Invalid login attempt");
            }
        }

        public async Task SetCurrentUser(User user)
        {
            await _localStorage.SetItemAsync("user", user);
        }

        public async Task<(User?, string)> CreateUser(User newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7000/api/user/CreateUser", newUser);
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    await SetCurrentUser(user);
                    return (user, string.Empty);
                }
                else
                {
                    return (null, "Error retrieving user information");
                }
            }
            else
            {
                return (null, "Invalid login attempt");
            }
        }
    }
}
