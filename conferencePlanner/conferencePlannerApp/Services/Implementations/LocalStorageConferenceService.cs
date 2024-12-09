﻿using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalStorageConferenceService : IConferenceService
    {
        public ILocalStorageService _localStorage;
        private const string StorageKey = "conferences";

        public LocalStorageConferenceService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
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
                var conferences = await _localStorage.GetItemAsync<List<Conference>>(StorageKey);
                return conferences ?? new List<Conference>();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to retrieve conferences.");
            }
        }
    }
}
