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
    public Task<User> LoginAsync(LoginModel loginModel)
    {
      var response = _httpClient.PostAsJsonAsync("/api/User/Login", loginModel);
      if (response.Result.IsSuccessStatusCode)
      {
        var user = response.Result.Content.ReadFromJsonAsync<User>().Result;
        if (user == null)
        {
          throw new Exception("Invalid login");
        }
        return Task.FromResult(user);
      }
      else
      {
        throw new Exception("Invalid login");
      }
    }

    public Task RegisterAsync(RegisterModel registerModel)
    {
      var response = _httpClient.PostAsJsonAsync("/api/User/Register", registerModel);
      if (response.Result.IsSuccessStatusCode)
      {
        return Task.CompletedTask;
      }
      else
      {
        throw new Exception("Invalid registration");
      }
    }

    public Task LogoutAsync()
    {
      throw new NotImplementedException();
    }
  }
}