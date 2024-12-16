using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using System.Net.Http.Json;

namespace conferencePlannerApp.Services.Implementations
{
	public class AbstractService : IAbstractService
	{
		private readonly HttpClient _httpClient;

		public AbstractService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<Abstract> AddAbstract(Abstract @abstract)
		{
        try 
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Abstract/CreateAbstract", @abstract);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create abstract: {errorContent}");
            }
						System.Console.WriteLine("Abstract added successfully" + response);
            
            var newAbstract = await response.Content.ReadFromJsonAsync<Abstract>();
            return newAbstract!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddAbstract: {ex.Message}");
            throw;
        }
		}

		public async Task<List<Abstract>> GetAbstracts()
		{
			var response = await _httpClient.GetAsync("/api/abstract/getallabstracts");
			response.EnsureSuccessStatusCode();
			var abstracts = await response.Content.ReadFromJsonAsync<List<Abstract>>();
			if (abstracts == null)
			{
				throw new InvalidOperationException("Failed to retrieve abstracts.");
			}
			return abstracts;
		}

		public Task UpdateAbstract(Abstract _abstract)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAbstract(Abstract _abstract)
		{
			throw new NotImplementedException();
		}

		public Task<Abstract> UpdateReview(int abstractId, Review review)
		{
			throw new NotImplementedException();
		}
	}
}
