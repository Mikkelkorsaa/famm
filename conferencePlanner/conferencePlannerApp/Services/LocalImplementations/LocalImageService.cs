using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

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
        throw new ArgumentNullException(nameof(file), "No file provided");

      if (file.Size == 0)
        throw new InvalidOperationException("File is empty");

      var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
      if (!allowedTypes.Contains(file.ContentType.ToLower()))
        throw new InvalidOperationException("Invalid file type. Only JPEG, PNG, and GIF are allowed.");

      try
      {
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        using var streamContent = new StreamContent(stream);

        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        content.Add(streamContent, "file", file.Name);

        var response = await _httpClient.PostAsync("api/images/upload", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Raw Response: {responseContent}"); // Debug logging

        if (response.IsSuccessStatusCode)
        {
          var options = new JsonSerializerOptions
          {
            PropertyNameCaseInsensitive = true
          };

          var result = JsonSerializer.Deserialize<ImageUploadResult>(responseContent, options);
          if (result == null)
            throw new InvalidOperationException("Failed to parse upload response");
          return result;
        }

        throw new InvalidOperationException(responseContent);
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException($"Failed to upload image: {ex.Message}", ex);
      }
    }

    public async Task<ImageResponse> GetImageAsync(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentException("File name cannot be empty", nameof(fileName));

      try
      {
        var response = await _httpClient.GetAsync($"api/images/get/{fileName}");

        switch (response.StatusCode)
        {
          case System.Net.HttpStatusCode.OK:
            var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
            var imageData = await response.Content.ReadAsByteArrayAsync();
            return new ImageResponse
            {
              Data = imageData,
              ContentType = contentType,
              FileName = fileName
            };

          case System.Net.HttpStatusCode.NotFound:
            throw new FileNotFoundException($"Image not found: {fileName}");

          case System.Net.HttpStatusCode.InternalServerError:
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Server error: {errorContent}");

          default:
            throw new InvalidOperationException($"Unexpected response: {response.StatusCode}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException("Failed to communicate with the server", ex);
      }
      catch (Exception ex) when (ex is not InvalidOperationException && ex is not FileNotFoundException)
      {
        throw new InvalidOperationException("An unexpected error occurred while retrieving the image", ex);
      }
    }

    public async Task DeleteImageAsync(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
        throw new ArgumentException("File name cannot be empty", nameof(fileName));

      try
      {
        var response = await _httpClient.DeleteAsync($"api/images/delete/{fileName}");

        switch (response.StatusCode)
        {
          case System.Net.HttpStatusCode.OK:
            return;

          case System.Net.HttpStatusCode.NotFound:
            throw new FileNotFoundException($"Image not found: {fileName}");

          case System.Net.HttpStatusCode.InternalServerError:
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Server error: {errorContent}");

          default:
            throw new InvalidOperationException($"Unexpected response: {response.StatusCode}");
        }
      }
      catch (HttpRequestException ex)
      {
        throw new InvalidOperationException("Failed to communicate with the server", ex);
      }
      catch (Exception ex) when (ex is not InvalidOperationException && ex is not FileNotFoundException)
      {
        throw new InvalidOperationException("An unexpected error occurred while deleting the image", ex);
      }
    }
  }
}