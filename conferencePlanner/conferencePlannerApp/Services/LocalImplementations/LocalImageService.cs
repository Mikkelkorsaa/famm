using System.Net.Http.Json;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.LocalImplementations
{
  public class LocalImageService : IImageService
  {
    private readonly HttpClient _httpClient;
    private readonly IApiAddressService _apiAddressService;

    public LocalImageService(HttpClient httpClient, IApiAddressService apiAddressService)
    {
      _httpClient = httpClient;
      _apiAddressService = apiAddressService;
    }

    public async Task<ImageUploadResult> UploadImageAsync(IBrowserFile file)
    {
      if (file == null)
        throw new ArgumentNullException(nameof(file));

      // Check file size (e.g., 10MB limit)
      if (file.Size > 10 * 1024 * 1024)
        throw new InvalidOperationException("File size exceeds 10MB limit.");

      // Check file type
      var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
      if (!allowedTypes.Contains(file.ContentType.ToLower()))
        throw new InvalidOperationException("Invalid file type. Only JPEG, PNG, and GIF are allowed.");

      try
      {
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        using var streamContent = new StreamContent(stream);

        content.Add(streamContent, "file", file.Name);

        var response = await _httpClient.PostAsync("api/images/upload", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ImageUploadResult>();
        if (result == null)
          throw new InvalidOperationException("Failed to parse upload response");

        return result;
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException($"Failed to upload image: {ex.Message}", ex);
      }
    }

    public async Task<byte[]> GetImageAsync(string fileName)
    {
      try
      {
        var response = await _httpClient.GetAsync($"api/images/get/{fileName}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException($"Failed to retrieve image: {ex.Message}", ex);
      }
    }

    public async Task DeleteImageAsync(string fileName)
    {
      try
      {
        var response = await _httpClient.DeleteAsync($"api/images/delete/{fileName}");
        response.EnsureSuccessStatusCode();
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException($"Failed to delete image: {ex.Message}", ex);
      }
    }
  }
}