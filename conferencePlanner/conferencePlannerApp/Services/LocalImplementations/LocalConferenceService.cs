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
                        Title = "Test1",
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
                                Comment = "Excellent application of machine learning in a critical area.",
                                Recommend = false
                            },
                            new Review
                            {
                                Id = 2,
                                UserId = 102,
                                Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 4 }, new Criteria { Name = "Originality", Grade = 5 } },
                                Comment = "Innovative approach with promising results.",
                                Recommend = true

                            }
                        }
                    }
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
                      Title = "Test 2",
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
                      Title = "Test 3",
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
        public LocalConferenceService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public Task CreateConferenceAsync(Conference conference)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Conference>> GetActiveConferencesAsync()
        {
            IEnumerable<Conference> Result = _conferences;
            return Task.FromResult(Result);
        }

        public Task<Conference> GetByIdAsync(int? id)
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

        public Task<List<Abstract>> GetAllAbstractsByIdAsync(int id)
        {
            var conference = _conferences.FirstOrDefault(c => c.Id == id);
            if (conference != null)
            {
                return Task.FromResult(conference.Abstracts);
            }
            else
            {
                throw new Exception("Conference not found");
            }
        }

        public Task UpdateReview(int abstractId, Review review)
        {
            var conference = _conferences.FirstOrDefault(c => c.Abstracts.Any(a => a.Id == abstractId));
            if (conference != null)
            {
                var abstractItem = conference.Abstracts.FirstOrDefault(a => a.Id == abstractId);
                if (abstractItem != null)
                {
                    var existingReview = abstractItem.Reviews.FirstOrDefault(r => r.Id == review.Id);
                    if (existingReview != null)
                    {
                        existingReview.UserId = review.UserId;
                        existingReview.Criterias = review.Criterias;
                        existingReview.Comment = review.Comment;
                    }
                    else
                    {
                        abstractItem.Reviews.Add(review);
                    }
                    return Task.CompletedTask;
                }
                else
                {
                    throw new Exception("Abstract not found");
                }
            }
            else
            {
                throw new Exception("Conference not found");
            }
        }
    }
}
