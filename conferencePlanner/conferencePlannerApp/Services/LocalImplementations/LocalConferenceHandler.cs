using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class LocalConferenceHandler : IConferenceHandler
    {
        public ILocalStorageService localStorage { get; set; }

        public LocalConferenceHandler(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }
        public async Task createConference(Conference conference)
        {
            List<Conference> conferences = await localStorage.GetItemAsync<List<Conference>>("conferences");
            if (conferences == null)
                conferences = new();
            conferences.Add(conference);
            await localStorage.SetItemAsync<List<Conference>>("conferences", conferences);
        }

        public async Task resetStorage() { 
            
        }

    }
}
