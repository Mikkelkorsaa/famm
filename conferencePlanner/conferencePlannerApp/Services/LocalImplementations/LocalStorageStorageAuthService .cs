using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using conferencePlannerApp.Services.RoleAutherization;
using Microsoft.AspNetCore.Components;

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
			return await _localStorage.GetItemAsync<User>(StorageKey);
		}

		public async Task SetCurrentUser(User user)
		{
			await _localStorage.SetItemAsync(StorageKey, user);
		
		}

		public async Task ClearCurrentUser()
		{
			await _localStorage.RemoveItemAsync(StorageKey);
			
		}
	}
}