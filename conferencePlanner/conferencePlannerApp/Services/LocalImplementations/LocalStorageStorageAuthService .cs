using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;

namespace conferencePlannerApp.Services.LocalImplementations 
{
    public class LocalStorageAuthService : IAuthService 
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "currentUser";

        public LocalStorageAuthService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<User?> GetCurrentUser()
        {
            return await _localStorage.GetItemAsync<User>(StorageKey) ?? null;
        }

        public async Task SetCurrentUser(User user)
        {
            await _localStorage.SetItemAsync(StorageKey, user);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(StorageKey);
        }

        public async Task<User?> Login(LoginModel loginModel)
        {
            return new User {
                Id = 0,
                Name = "admin",
                Email = "admin",
                Password = "1234",
                Role = 0,
                Organization = "admin",
                CreatedAt = DateTime.Now,
                IsActive = true
            };
        }

        public async Task<User> CreateUser(User user)
        {
            return new User
            {
                Id = 0,
                Name = "admin",
                Email = "admin",
                Password = "1234",
                Role = 0,
                Organization = "admin",
                CreatedAt = DateTime.Now,
                IsActive = true
            };
        }
    }
}