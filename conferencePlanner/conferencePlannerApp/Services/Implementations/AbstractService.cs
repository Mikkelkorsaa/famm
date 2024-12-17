using System.Net.Http.Json;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
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

		public async Task<bool> UpdateAbstract(Abstract _abstract)
		{
			var response = await _httpClient.PutAsJsonAsync("/api/Abstract/UpdateAbstract/", _abstract);
			response.EnsureSuccessStatusCode();
			return true;
        }

        public Task DeleteAbstract(Abstract _abstract)
		{
			throw new NotImplementedException();
		}

		public Task<Abstract> UpdateReview(int abstractId, Review review)
		{
			throw new NotImplementedException();
		}

        public async Task<List<Abstract>> GetAllAbstractsByConferenceIdAsync(int conferenceId)
        {
			var response = await GetAbstracts();
			var result = response.Where(item => item.ConferenceId == conferenceId).ToList();
			if (result != null)
				return result;
			else throw new Exception("No abstract with the given Id");
        }

        public async Task<bool> HasReviewAsync(int abstractId, int userId)
        {
			var abstractItem = await GetById(abstractId);

            
			if (abstractItem != null)
			{
				var review = abstractItem.Reviews.FirstOrDefault(r => r.UserId == userId);
				if (review != null) return true;
				else return false;
			}
			else throw new Exception("No abstract with that abstractId");
        }

        public async Task<int> GetNextReviewIdAsync(int abstractId)
        {
            var response = await GetById(abstractId);
            if (response != null)
            {
                if (response.Reviews.Any())
                {
                    int latestId = response.Reviews.Max(abs => abs.Id);
                    int newId = latestId + 1;
                    return newId;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                throw new Exception("No abstract with the given Id");
            }
        }


        public async Task<Abstract> GetById(int abstractId)
        {
			var response = await _httpClient.GetAsync($"/api/Abstract/GetAbstractById/{abstractId}");
			var abs = await response.Content.ReadFromJsonAsync<Abstract>();
			if (abs == null) throw new Exception("No abstract with the given id");
			else return abs;
        }

        public async Task<Review> GetExistingReviewAsync(int abstractId, int userId)
        {
            var respose = await GetById(abstractId);
            if (respose != null)
            {
                var review = respose.Reviews.FirstOrDefault(r => r.UserId == userId);
                if (review != null) return review;
                else throw new Exception("No review with the given userId");
            }
            else throw new Exception("No abstract with the given abstractId");
        }
	}
}
