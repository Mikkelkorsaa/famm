using System.Net.Http.Json;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Implementations
{
  public class LoginService : ILoginService
  {
    private readonly HttpClient _httpClient;

    public LoginService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<User> LoginAsync(LoginModel loginModel)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("/api/User/Login", loginModel);
        if (response.IsSuccessStatusCode)
        {
          var user = await response.Content.ReadFromJsonAsync<User>();
          if (user == null)
          {
            throw new Exception("Invalid login");
          }
          return user;
        }
        throw new Exception("Invalid login");
      }
      catch (Exception ex)
      {
        throw new Exception($"Login failed: {ex.Message}");
      }
    }

    public async Task RegisterAsync(RegisterModel registerModel)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync("/api/User/Register", registerModel);
        if (!response.IsSuccessStatusCode)
        {
          var errorContent = await response.Content.ReadAsStringAsync();
          throw new Exception($"Registration failed: {errorContent}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Registration error details: {ex.Message}");
        throw;
      }
    }
  }
}