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
                    "Practical Impact",
                    "TEEEEEST"
                },
                Location = new Venue
                {
                Id = 1,
                Name = "Grand Convention Center",
                Address = "123 Main Street, Cityville",
                Rooms = new List<Room>
                {
                    new Room { Id = 1, Name = "Main Hall" },
                    new Room { Id = 2, Name = "Conference Room A" },
                    new Room { Id = 3, Name = "Conference Room B" }
                }
                },
                Abstracts = new List<Abstract>
                {
                    new Abstract
                    {
                        Id = 1,
                        ConferenceId = 1,
                        SenderName = "Dr. Sarah Johnson",
                        PresenterEmail = "s.johnson@university.edu",
                        CoAuthors = new List<string> { "Dr. Michael Chen", "Prof. Emma Williams" },
                        Organization = "University of Technology",
                        Title = "Machine Learning Applications in Climate Change Prediction",
                        KeyValues = "climate modeling, machine learning, neural networks, weather prediction",
                        AbstractText = "This study presents a novel approach to climate change prediction using advanced machine learning techniques. We demonstrate how neural networks can be applied to historical climate data to improve the accuracy of future climate projections. Our results show a 15% improvement in prediction accuracy compared to traditional methods.",
                        Category = "Machine Learning",
                        Picture = "string.Empty",
                        Reviews = new List<Review>
                        {
                            new Review
                            {
                                Id = 1,
                                UserId = 101,
                                Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 5 }, new Criteria { Name = "Originality", Grade = 4 } },
                                Comment = "Excellent application of machine learning in a critical area."
                            },
                            new Review
                            {
                                Id = 2,
                                UserId = 102,
                                Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 4 }, new Criteria { Name = "Originality", Grade = 5 } },
                                Comment = "Innovative approach with promising results."
                            }
                        }
                    }
                }
            },
            new Conference
            {
              Id = 2,
              Name = "European Data Science Summit 2024",
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
              },
              Abstracts = new List<Abstract>
              {
                  new Abstract
                  {
                      Id = 2,
                      ConferenceId = 2,
                      SenderName = "Prof. David Martinez",
                      PresenterEmail = "dmartinez@research.org",
                      CoAuthors = new List<string> { "Dr. Lisa Cooper" },
                      Organization = "Research Institute of Biotechnology",
                      Title = "Novel CRISPR Applications in Treating Genetic Disorders",
                      KeyValues = "CRISPR, gene editing, genetic disorders, therapeutic applications",
                      AbstractText = "Our research explores innovative applications of CRISPR technology in treating rare genetic disorders. Through a series of controlled experiments, we have developed a modified CRISPR-Cas9 system that shows promising results in correcting specific genetic mutations with minimal off-target effects.",
                      Category = "Biotechnology",
                      Picture = "string.Empty",
                      Reviews = new List<Review>
                      {
                          new Review
                          {
                              Id = 3,
                              UserId = 103,
                              Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 5 }, new Criteria { Name = "Originality", Grade = 4 } },
                              Comment = "Groundbreaking research with significant potential."
                          },
                          new Review
                          {
                              Id = 4,
                              UserId = 104,
                              Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 4 }, new Criteria { Name = "Originality", Grade = 5 } },
                              Comment = "Innovative approach with promising results."
                          }
                      }
                  }
              }
            },
            new Conference
            {
              Id = 3,
              Name = "Asia-Pacific Cybersecurity Conference 2024",
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
              },
              Abstracts = new List<Abstract>
              {
                  new Abstract
                  {
                      Id = 3,
                      ConferenceId = 3,
                      SenderName = "Dr. Rachel Anderson",
                      PresenterEmail = "anderson.r@sustaintech.com",
                      CoAuthors = new List<string> { "Dr. James Wilson", "Dr. Maria Garcia", "Dr. Tom Baker" },
                      Organization = "SustainTech Solutions",
                      Title = "Sustainable Urban Development: A Smart City Framework",
                      KeyValues = "smart cities, sustainability, urban planning, IoT integration",
                      AbstractText = "This paper presents a comprehensive framework for implementing smart city solutions in urban development. By integrating IoT sensors, renewable energy systems, and adaptive traffic management, our approach has demonstrated significant improvements in urban efficiency and sustainability. Case studies from three major cities show reductions in energy consumption and traffic congestion.",
                      Category = "Urban Development",
                      Picture = "string.Empty",
                      Reviews = new List<Review>
                      {
                          new Review
                          {
                              Id = 5,
                              UserId = 105,
                              Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 3 }, new Criteria { Name = "Originality", Grade = 4 } },
                              Comment = "Innovative approach with promising results."
                          },
                          new Review
                          {
                              Id = 6,
                              UserId = 106,
                              Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 4 }, new Criteria { Name = "Originality", Grade = 5 } },
                              Comment = "Excellent application of smart city technologies."
                          }
                      }
                  }
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
            var response = _conferences.FirstOrDefault(c => c.Id == conference.Id);
            if (response == null)
            {
                var newConference = conference with { Id = ++_lastId };
                _conferences.Add(newConference);
                return await Task.FromResult(newConference);
            }
            else
                throw new Exception("Conference ID already exists");
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

        public Task<List<string>> ListAllCriteria(Conference conference)
        {
            throw new NotImplementedException();
        }
    }
}
