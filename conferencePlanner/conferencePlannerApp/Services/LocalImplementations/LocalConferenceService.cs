using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components;


namespace conferencePlannerApp.Services.LocalImplementations
{

    public class LocalConferenceService : IConferenceService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "currentConferenceId";
        private readonly List<string> _reviewCriteria = new();
        private readonly List<Conference> _conferences = new()

        {
            new Conference
            {
                Id = 1,
                Name = "International Software Engineering Conference 2024",
                AbstractDeadLine = new DateTime(2025, 6, 15),
                ReviewDeadline = new DateTime(2025, 7, 15),
                StartDate = new DateTime(2025, 9, 20),
                EndDate = new DateTime(2025, 9, 23),
                Category = new List<string>
                {
                    "Software Engineering",
                    "Artificial Intelligence",
                    "Cloud Computing"
                },
                ReviewCriteria = new List<string>
                {
                    "Technical Contribution",
                    "Originality",
                    "Presentation Quality",
                    "Practical Impact"
                },
                Location = new Venue
                {
                  Id = 1,
                  Name = "Grand Tech Conference Center",
                  Address = "123 Innovation Boulevard, San Francisco, CA 94105, USA"
                }
            },
            new Conference
            {
              Id = 2,
              Name = "European Data Science Summit 2024 test 2",
              AbstractDeadLine = new DateTime(2025, 7, 30),
              ReviewDeadline = new DateTime(2025, 8, 30),
              StartDate = new DateTime(2025, 10, 15),
              EndDate = new DateTime(2025, 10, 18),
              Category = new List<string>
                {
                    "Data Science",
                    "Machine Learning",
                    "Big Data Analytics"
                },
              ReviewCriteria = new List<string>
                {
                    "Methodology",
                    "Innovation",
                    "Research Impact",
                    "Data Quality"
                },
              Location = new Venue
              {
                Id = 2,
                Name = "Berlin Congress Center",
                Address = "456 Data Street, 10117 Berlin, Germany"
              }

            },
            new Conference
            {
              Id = 3,
              Name = "Asia-Pacific Cybersecurity Conference 2024 test 3",
              AbstractDeadLine = new DateTime(2025, 5, 1),
              ReviewDeadline = new DateTime(2025, 6, 1),
              StartDate = new DateTime(2025, 8, 10),
              EndDate = new DateTime(2025, 8, 12),
              Category = new List<string>
                {
                    "Cybersecurity",
                    "Network Security",
                    "Digital Forensics"
                },
              ReviewCriteria = new List<string>
                {
                    "Technical Depth",
                    "Security Impact",
                    "Implementation Feasibility",
                    "Novel Approach"
                },
              Location = new Venue
              {
                Id = 3,
                Name = "Singapore Tech Hub",
                Address = "789 Cyber Road, Downtown Core, Singapore 018956"
              }

            }
        };
        public LocalConferenceService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public Task<List<Conference>> GetActiveConferencesAsync()
        {
            IEnumerable<Conference> Result = _conferences;
            return Task.FromResult(Result.ToList());
        }

        public Task<Conference> GetByIdAsync(int id)
        {
            var result = _conferences.FirstOrDefault(item => item.Id == id);
            if (result != null)
                return Task.FromResult(result);
            else
                throw new Exception("Id doesnt match any existing conferences");
        }

        public async Task<int?> GetCurrentConferenceIdAsync()
        {

            var conference = await _localStorage.GetItemAsync<int?>(StorageKey);
            if (conference == null)
                return null;
            else
                return conference;
        }

        public async Task<Conference> SetCurrentConferenceAsync(int id)
        {

            await _localStorage.SetItemAsync(StorageKey, id);

            var conference = await GetByIdAsync(id);
            return conference;

        }

        public Task<List<string>> GetCriteriaByIdAsync(int conferenceId)
        {
            var conference = _conferences.FirstOrDefault(c => c.Id == conferenceId);
            if (conference != null)
            {
                return Task.FromResult(conference.ReviewCriteria);
            }
            else
            {
                throw new Exception("Conference not found");
            }
        }

        public Task<Conference> CreateConferenceAsync(Conference conference)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conference>> GetConferencesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Conference> UpdateAsync(Conference conference)
        {
            throw new NotImplementedException();
        }

        Task<int> IConferenceService.GetCurrentConferenceIdAsync()
        {
            throw new NotImplementedException();
        }

        Task IConferenceService.SetCurrentConferenceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetCategoriesByIdAsync(int conferenceId)
        {
            throw new NotImplementedException();
        }
    }
}
