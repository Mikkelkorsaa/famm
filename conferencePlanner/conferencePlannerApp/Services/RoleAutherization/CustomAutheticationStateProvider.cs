using Blazored.LocalStorage;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace conferencePlannerApp.Services.RoleAutherization
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _localStorage.GetItemAsync<User>("currentUser");

            if (user == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            

            var claims = new[] { new Claim(ClaimTypes.Role, user.Role.ToString()) };

            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }
    }
}
