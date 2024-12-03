using conferencePlannerApp.Services.LocalImplementations;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class LocalAbstractService : IAbstractService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "localAbstrsactList";
        List<Abstract> _abstracts = new List<Abstract>();
        List<Abstract> dummyAbstracts = new List<Abstract>
            {
                new Abstract
                {
                    Id = 1,
                    ConferenceId = 101,
                    SenderName = "John Doe",
                    PresenterEmail = "john.doe@example.com",
                    CoAuthors = new List<string> { "Jane Smith", "Bob Johnson" },
                    Organization = "Tech University",
                    Title = "Innovations in AI",
                    KeyValues = "AI, Machine Learning, Innovation",
                    AbstractText = "This paper discusses the latest innovations in AI and machine learning.",
                    Category = "Technology",
                    Picture = new byte[] { }, // Add byte data for the picture if needed
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            Id = 1,
                            UserId = 123,
                            Comment = "Great insights on AI!",
                            Criterias = new List<Criteria>
                            {
                                new Criteria { Name = "Content Quality", Grade = 5 },
                                new Criteria { Name = "Speaker Performance", Grade = 4 },
                                new Criteria { Name = "Organization", Grade = 5 }
                            }
                        },
                        new Review
                        {
                            Id = 2,
                            UserId = 456,
                            Comment = "Very informative.",
                            Criterias = new List<Criteria>
                            {
                                new Criteria { Name = "Content Quality", Grade = 4 },
                                new Criteria { Name = "Speaker Performance", Grade = 5 },
                                new Criteria { Name = "Organization", Grade = 4 }
                            }
                        }
                    }
                },
                new Abstract
                {
                    Id = 2,
                    ConferenceId = 102,
                    SenderName = "Alice Brown",
                    PresenterEmail = "alice.brown@example.com",
                    CoAuthors = new List<string> { "Charlie Davis" },
                    Organization = "Innovation Labs",
                    Title = "Blockchain Technology",
                    KeyValues = "Blockchain, Cryptocurrency, Security",
                    AbstractText = "This paper explores the applications of blockchain technology in various industries.",
                    Category = "Finance",
                    Picture = new byte[] { }, // Add byte data for the picture if needed
                    Reviews = new List<Review>
                    {
                        new Review
                        {
                            Id = 3,
                            UserId = 789,
                            Comment = "Excellent coverage of blockchain.",
                            Criterias = new List<Criteria>
                            {
                                new Criteria { Name = "Content Quality", Grade = 5 },
                                new Criteria { Name = "Speaker Performance", Grade = 4 },
                                new Criteria { Name = "Organization", Grade = 5 }
                            }
                        },
                        new Review
                        {
                            Id = 4,
                            UserId = 101,
                            Comment = "Good insights, but could be more detailed.",
                            Criterias = new List<Criteria>
                            {
                                new Criteria { Name = "Content Quality", Grade = 4 },
                                new Criteria { Name = "Speaker Performance", Grade = 3 },
                                new Criteria { Name = "Organization", Grade = 4 }
                            }
                        }
                    }
                }
            };

        public LocalAbstractService(ILocalStorageService localstorage)
        {
            _localStorage = localstorage;
        }

        private async Task UpdateLocalStorage() => await _localStorage.SetItemAsync<List<Abstract>>(StorageKey, _abstracts);

        public Task AddAbstract(Abstract _abstract)
        {
            _abstracts.Add(_abstract);
            return UpdateLocalStorage();
        }

        public async Task<List<Abstract>> GetAllAbstracts()
        {
            return dummyAbstracts;
        }

        public Task UpdateAbstract(Abstract _abstract)
        {
            throw new NotImplementedException();
        }
    }
}
