using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;
namespace conferencePlannerApi.Repositories.LocalImplementations
{
    public class LocalConferenceRepo : IConferenceRepo
    {
        private readonly List<Conference> _conferences = new()
        {
            new Conference
            {
                Id = 1,
                Name = "International Software Engineering Conference 2024",
                AbstractDeadLine = new DateTime(2024, 6, 15),
                ReviewDeadline = new DateTime(2024, 7, 15),
                StartDate = new DateTime(2024, 9, 20),
                EndDate = new DateTime(2024, 9, 23),
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
              Name = "European Data Science Summit 2024",
              AbstractDeadLine = new DateTime(2024, 7, 30),
              ReviewDeadline = new DateTime(2024, 8, 30),
              StartDate = new DateTime(2024, 10, 15),
              EndDate = new DateTime(2024, 10, 18),
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
              Name = "Asia-Pacific Cybersecurity Conference 2024",
              AbstractDeadLine = new DateTime(2024, 5, 1),
              ReviewDeadline = new DateTime(2024, 6, 1),
              StartDate = new DateTime(2024, 8, 10),
              EndDate = new DateTime(2024, 8, 12),
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
        private int _lastId = 3;


        public async Task<Conference> GetByIdAsync(int id)
        {
            var response = _conferences.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(response != null ? response : throw new Exception("Conference not found"));
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var result = _conferences;
            return await Task.FromResult(result.Any() ? result : throw new Exception("No conferences found"));
        }

        public async Task<Conference> CreateAsync(Conference conference)
        {
            return await Task.FromResult<Conference>(conference);
        }

        public async Task<Conference> UpdateAsync(Conference conference)
        {
            var existing = _conferences.FirstOrDefault(c => c.Id == conference.Id);
            if (existing == null)
                throw new Exception("Conference not found");

            existing.Name = conference.Name;
            existing.StartDate = conference.StartDate;
            existing.EndDate = conference.EndDate;

            return await Task.FromResult(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = _conferences.FirstOrDefault(c => c.Id == id);
            if (existing == null)
                throw new Exception("Conference not found");

            _conferences.Remove(existing);
            return await Task.FromResult(true);
        }

        public Task<List<string>> ListAllCriteria(int conferenceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ListAllCategories(int conferenceId)
        {
            throw new NotImplementedException();
        }
    }
}
