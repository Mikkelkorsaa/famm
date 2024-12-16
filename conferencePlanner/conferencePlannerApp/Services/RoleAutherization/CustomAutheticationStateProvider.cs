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
		Console.WriteLine($"Current user: {user?.ToString() ?? "null"}");
		Console.WriteLine($"Current user role: {user?.Role.ToString() ?? "null"}");

		if (user == null)
		{
			Console.WriteLine("No user found - returning empty AuthenticationState");
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		var claims = new List<Claim>
	{
		new Claim(ClaimTypes.Role, user.Role.ToString()),
        // Add a name claim as well
        new Claim(ClaimTypes.Name, user.Name ?? "anonymous")
	};

		// Print all claims for debugging
		foreach (var claim in claims)
		{
			Console.WriteLine($"Adding claim: {claim.Type}: {claim.Value}");
		}

		var identity = new ClaimsIdentity(claims, "Bearer");
		var principal = new ClaimsPrincipal(identity);

		// Verify the principal has the role
		Console.WriteLine($"Is in role {user.Role}: {principal.IsInRole(user.Role.ToString())}");

		return new AuthenticationState(principal);
	}
}
