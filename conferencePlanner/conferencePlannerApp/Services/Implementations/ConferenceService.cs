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

		public async Task<Conference> CreateConferenceAsync(Conference conference)
		{
			var response = await _httpClient.PostAsJsonAsync("/api/Conference/CreateConference", conference);
			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Failed to create abstract: {errorContent}");
			}
			if (response.IsSuccessStatusCode)
			{
				var newConference = await response.Content.ReadFromJsonAsync<Conference>();
				return newConference!;
			}
			else
			{
				throw new Exception("something went wrong");
			}

		}
		public async Task<List<Conference>> GetConferencesAsync()
		{
			try
			{
				var conferences = await _httpClient.GetAsync("/api/Conference/GetAllConferences");
				conferences.EnsureSuccessStatusCode();
				var result = await conferences.Content.ReadFromJsonAsync<IEnumerable<Conference>>();
				if (result == null)
				{
					throw new InvalidOperationException("Failed to retrieve conferences.");
				}
				return result.ToList();
			}
			catch (Exception)
			{
				throw new InvalidOperationException("Failed to retrieve conferences.");
			}
		}

		public async Task<List<Conference>> GetActiveConferencesAsync()
		{
			try
			{
				var conferences = await _httpClient.GetAsync("/api/Conference/GetActiveConferences");
				conferences.EnsureSuccessStatusCode();
				var result = await conferences.Content.ReadFromJsonAsync<IEnumerable<Conference>>();
				if (result == null)
				{
					throw new InvalidOperationException("Failed to retrieve conferences.");
				}
				return result.ToList();
			}
			catch (Exception)
			{
				throw new InvalidOperationException("Failed to retrieve conferences.");
			}
		}

		public async Task<Conference> GetByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"/api/Conference/GetConferenceById/{id}");
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<Conference>();
			if (result == null) throw new Exception("No conference found");
			else return result;
		}


		public async Task SetCurrentConferenceAsync(int id)
		{
			await _localStorage.SetItemAsync(StorageKey, id);
		}

		public async Task<int> GetCurrentConferenceIdAsync()
		{
			var conference = await _localStorage.GetItemAsync<int>(StorageKey);
			return conference;
		}
		public async Task<List<string>> GetCriteriaByIdAsync(int conferenceId)
		{
			var response = await _httpClient.GetAsync($"/api/Conference/AllCriteria/{conferenceId}");


			var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();

			if (result == null)
			{
				throw new InvalidOperationException("Failed to retrieve criteria.");
			}
			else return result.ToList();

		}

		public async Task<Conference> UpdateAsync(Conference conference)
		{
			var response = await _httpClient.PutAsJsonAsync("/api/conference/UpdateConference", conference);
			var result = await response.Content.ReadFromJsonAsync<Conference>();
			if (result == null) throw new Exception("No conference found");
			else return result;
		}

		public async Task<List<string>> GetCategoriesByIdAsync(int conferenceId)
		{
			var response = await _httpClient.GetAsync($"/api/Conference/AllCategories/{conferenceId}");

			var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();

			if (result == null)
			{
				throw new InvalidOperationException("Failed to retrieve criteria.");
			}
			else return result.ToList(); ;
		}
	}
}
