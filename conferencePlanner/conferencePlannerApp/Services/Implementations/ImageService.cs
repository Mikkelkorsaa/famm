using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Implementations
{
  public class ImageService : IImageService
  {
    private readonly HttpClient _httpClient;

    public ImageService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ImageUploadResult> UploadImageAsync(IBrowserFile file)
    {
      try
      {
        using var content = new MultipartFormDataContent();
        using var stream = new MemoryStream();
        await file.OpenReadStream(maxAllowedSize: 10485760).CopyToAsync(stream); // 10MB max
        stream.Position = 0;

        using var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

        content.Add(streamContent, "file", file.Name);

        var response = await _httpClient.PostAsync("/api/Images/upload", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ImageUploadResult>()
            ?? throw new ImageServiceException("Failed to deserialize upload response", null);
      }
      catch (Exception ex) when (ex is HttpRequestException or IOException)
      {
        throw new ImageServiceException("Failed to upload image", ex);
      }
    }

    public async Task<ImageResponse> GetImageAsync(string fileName)
    {
      try
      {
        var response = await _httpClient.GetAsync($"/api/Images/get/{fileName}");
        response.EnsureSuccessStatusCode();

        return new ImageResponse
        {
          Content = await response.Content.ReadAsByteArrayAsync(),
          ContentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream"
        };
      }
      catch (HttpRequestException ex)
      {
        throw new ImageServiceException($"Failed to retrieve image: {fileName}", ex);
      }
    }

    public async Task DeleteImageAsync(string fileName)
    {
      try
      {
        var response = await _httpClient.DeleteAsync($"/api/Images/delete/{fileName}");
        response.EnsureSuccessStatusCode();
      }
      catch (HttpRequestException ex)
      {
        throw new ImageServiceException($"Failed to delete image: {fileName}", ex);
      }
    }
  }
}