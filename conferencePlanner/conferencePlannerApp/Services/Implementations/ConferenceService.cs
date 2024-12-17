using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Radzen;
using System.Net.Http.Json;


namespace conferencePlannerApp.Services.Implementations
{
    public class ConferenceService : IConferenceService
    {
        private readonly HttpClient _httpClient;
        public ILocalStorageService _localStorage;
        private const string StorageKey = "conferences";


        public ConferenceService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public async Task CreateConferenceAsync(Conference conference)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(conference);

                var conferences = await GetConferencesAsync();
                conferences.Add(conference);
                await _localStorage.SetItemAsync(StorageKey, conferences);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to create conference.");
            }
        }

        public async Task<IEnumerable<Conference>> GetActiveConferencesAsync()
        {
            try
            {
                var conferences = await GetConferencesAsync();
                var denmarkZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Copenhagen");
                var currentDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, denmarkZone);

                return conferences
                    .Where(c => c.EndDate > currentDate)
                    .ToList();
            }
            catch (TimeZoneNotFoundException)
            {
                throw new InvalidOperationException("Failed to determine timezone.");
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to retrieve active conferences.");
            }
        }

        private async Task<List<Conference>> GetConferencesAsync()
        {
            try
            {
                var conferences = await _httpClient.GetAsync("/api/Conference/GetAllConferences");
                conferences.EnsureSuccessStatusCode();
                var result = await conferences.Content.ReadFromJsonAsync<List<Conference>>();
                if(result == null)
                {
                    throw new InvalidOperationException("Failed to retrieve conferences.");
                }
                return result;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to retrieve conferences.");
            }
        }

        public async Task<Conference> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/conference/GetConferenceById/{id}");
            var result = await response.Content.ReadFromJsonAsync<Conference>();
            if (result == null) throw new Exception("No conference found");
            else return result;
        }

       
        public async Task<Conference> SetCurrentConferenceAsync(int id)
        {
            await _localStorage.SetItemAsync(StorageKey, id);

            var conference = await GetByIdAsync(id);
            return conference;
        }

        public async Task<int?> GetCurrentConferenceIdAsync()
        {
            var conference = await _localStorage.GetItemAsync<int?>(StorageKey);
            if (conference == null)
                return null;
            else
                return conference;
        }

        public Task<List<Abstract>> GetAllAbstractsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReview(int abstractId, Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetCriteriaByIdAsync(int conferenceId)
        {
            var response = await _httpClient.GetAsync($"/api/Conference/Conference/AllCriteria/{conferenceId}");


            var result = await response.Content.ReadFromJsonAsync<List<string>>();

            if(result == null)
            {
                throw new InvalidOperationException("Failed to retrieve criteria.");
            }
            else return result;

        }

        public Task<int> GetNextReviewIdAsync(int abstractId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasReviewAsync(int? abstractId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetExistingReviewAsync(int abstractId, int userId)
        {
            throw new NotImplementedException();
        }

		public async Task<Conference> UpdateAsync(Conference conference)
		{
			var response = await _httpClient.PutAsJsonAsync("/api/conference/UpdateConference", conference);
			var result = await response.Content.ReadFromJsonAsync<Conference>();
			if (result == null) throw new Exception("No conference found");
			else return result;
		}
	}
}
