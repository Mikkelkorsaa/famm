﻿using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Radzen;
using System.Net.Http.Json;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalStorageConferenceService : IConferenceService
    {
        private readonly HttpClient _httpClient;
        public ILocalStorageService _localStorage;
        private const string StorageKey = "conferences";

        public LocalStorageConferenceService(ILocalStorageService localStorage, HttpClient httpClient)
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
                var conferences = await _httpClient.GetAsync("/api/conference/GetAllConferences");
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

        public Task<Conference> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Conference> GetCurrentConferenceIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Conference> SetCurrentConferenceAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int?> IConferenceService.GetCurrentConferenceIdAsync()
        {
            throw new NotImplementedException();
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
            var response = await _httpClient.GetAsync($"/api/Conference/AllCriteria/{conferenceId}");


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
    }
}
