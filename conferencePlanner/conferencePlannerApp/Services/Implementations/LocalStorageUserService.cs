using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalStorageUserService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "currentUser";
        private const string IdStorageKey = "currentUserId";
        private int userId;

        public LocalStorageUserService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<User> GetCurrentUser()
        {
            try
            {
                return await _localStorage.GetItemAsync<User>(StorageKey);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to retrieve current user.");
            }
        }

        public async Task SetCurrentUser(User user)
        {
            try
            {
                await _localStorage.SetItemAsync(StorageKey, user);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to save current user.");
            }
        }
        public async Task SetCurrentUserId(User user)
        {
            userId = user.Id;
            await _localStorage.SetItemAsync(IdStorageKey, userId);

        }

        public async Task ClearCurrentUser()
        {
            try
            {
                await _localStorage.RemoveItemAsync(StorageKey);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to clear current user.");
            }
        }
    }
}