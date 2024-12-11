using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using System.Linq.Expressions;
using System.Net.Http;

namespace conferencePlannerApp.Services.Implementations
{
    public class ConferenceServiceImpl : IConferenceService
    {
        public ILocalStorageService LocalStorage { get; set; }
        public HttpClient HttpClient { get; set; }
        public Conference? Conference { get; set; } = null;

        public ConferenceServiceImpl(ILocalStorageService localStorageService, HttpClient http) { 
            LocalStorage = localStorageService;
            HttpClient = http;
        }
        public async Task<Conference> CreateConferenceAsync(Conference conference)
        {
            try {
                var response = await HttpClient.GetAsync("/api/user/CreateConference");
                
            }
            catch { }
        }

        public async Task<List<Conference>> GetActiveConferencesAsync(ConferenceFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetCategoriesAsync(int conferenceId)
        {
            throw new NotImplementedException();
        }
    }
}
