using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

using System.Net.Http.Json;

namespace conferencePlannerApp.Services.Implementations;

public class VenueService : IVenueService
{
    private readonly HttpClient _httpClient;

    public VenueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Venue> GetByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Venue/GetVenueById/{id}");
            response.EnsureSuccessStatusCode();
            var venue = await response.Content.ReadFromJsonAsync<Venue>();
            if (venue == null)
            {
                throw new InvalidOperationException($"Failed to retrieve venue with ID {id}.");
            }
            return venue;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
            throw;
        }
    }

}

