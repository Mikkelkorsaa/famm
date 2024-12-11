using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace conferencePlannerApp.Services.RoleAutherization;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly ILocalStorageService _localStorage;
	private readonly IAuthService _authService;

	public CustomAuthenticationStateProvider(ILocalStorageService localStorage, IAuthService authservice)
	{
		_localStorage = localStorage;
		_authService = authservice;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var user = await _authService.GetCurrentUser();

		if (user == null)
		{
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		var claims = new List<Claim> { new Claim(ClaimTypes.Role, user.Role.ToString()) };

		var identity = new ClaimsIdentity(claims, "custom");
		var principal = new ClaimsPrincipal(identity);

		return new AuthenticationState(principal);
	}
}
