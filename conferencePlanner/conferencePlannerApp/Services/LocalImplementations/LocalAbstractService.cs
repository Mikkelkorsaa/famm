using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Implementations
{
    public class LocalAbstractService : IAbstractService
    {
        private readonly List<Abstract> _abstracts;
        private int _nextId = 4; // Start after our predefined abstracts

        public LocalAbstractService()
        {
            _abstracts = new List<Abstract>
            {
                new Abstract
                {
                    Id = 1,
                    ConferenceId = 1,
                    SenderName = "Dr. Sarah Johnson",
                    PresenterEmail = "sarah.johnson@university.edu",
                    CoAuthors = new List<string> { "Dr. Michael Chen", "Prof. Emma Williams" },
                    Organization = "University of Science",
                    Title = "Machine Learning Applications in Climate Change Prediction",
                    KeyValues = "machine learning, climate change, predictive modeling",
                    AbstractText = "This study presents a novel approach to climate change prediction using advanced machine learning algorithms...",
                    Category = "Data Science",
                    Picture = "profile1.jpg",
                    Reviews = new List<Review>()
                },
                new Abstract
                {
                    Id = 2,
                    ConferenceId = 1,
                    SenderName = "Prof. David Martinez",
                    PresenterEmail = "d.martinez@techlab.com",
                    CoAuthors = new List<string> { "Dr. Lisa Brown" },
                    Organization = "Tech Research Lab",
                    Title = "Quantum Computing in Cryptography",
                    KeyValues = "quantum computing, cryptography, security",
                    AbstractText = "An exploration of how quantum computing developments will impact current cryptographic methods...",
                    Category = "Quantum Computing",
                    Picture = "profile2.jpg",
                    Reviews = new List<Review>()
                },
                new Abstract
                {
                    Id = 3,
                    ConferenceId = 1,
                    SenderName = "Dr. Emily Parker",
                    PresenterEmail = "eparker@biotech.org",
                    CoAuthors = new List<string> { "Dr. James Wilson", "Dr. Maria Garcia", "Dr. Robert Lee" },
                    Organization = "BioTech Institute",
                    Title = "Advances in CRISPR Gene Editing Technology",
                    KeyValues = "CRISPR, gene editing, biotechnology",
                    AbstractText = "Recent developments in CRISPR technology have opened new possibilities in genetic research...",
                    Category = "Biotechnology",
                    Picture = "profile3.jpg",
                    Reviews = new List<Review>()
                }
            };
        }

        public async Task<List<Abstract>> GetAbstracts()
        {
            return await Task.FromResult(_abstracts.ToList());
        }

        public async Task<Abstract> AddAbstract(Abstract @abstract)
        {
            var newAbstract = @abstract with { Id = _nextId++ };
            _abstracts.Add(newAbstract);
            return await Task.FromResult(newAbstract);
        }

        public async Task<bool> UpdateAbstract(Abstract @abstract)
        {
            var existingAbstract = _abstracts.FirstOrDefault(a => a.Id == @abstract.Id);
            if (existingAbstract == null)
            {
                return false;
                throw new KeyNotFoundException($"Abstract with ID {@abstract.Id} not found.");
                
            }

            var index = _abstracts.IndexOf(existingAbstract);
            _abstracts[index] = @abstract;
            await Task.CompletedTask;
            return true;
        }

        public async Task DeleteAbstract(Abstract @abstract)
        {
            var existingAbstract = _abstracts.FirstOrDefault(a => a.Id == @abstract.Id);
            if (existingAbstract == null)
            {
                throw new KeyNotFoundException($"Abstract with ID {@abstract.Id} not found.");
            }

            _abstracts.Remove(existingAbstract);
            await Task.CompletedTask;
        }

        public async Task<Abstract> UpdateReview(int abstractId, Review review)
        {
            var @abstract = _abstracts.FirstOrDefault(a => a.Id == abstractId);
            if (@abstract == null)
            {
                throw new KeyNotFoundException($"Abstract with ID {abstractId} not found.");
            }

            var existingReview = @abstract.Reviews.FirstOrDefault(r => r.Id == review.Id);
            if (existingReview != null)
            {
                var index = @abstract.Reviews.IndexOf(existingReview);
                @abstract.Reviews[index] = review;
            }
            else
            {
                review.Id = @abstract.Reviews.Count + 1;
                @abstract.Reviews.Add(review);
            }

            return await Task.FromResult(@abstract);
        }

        public Task<List<Abstract>> GetAllAbstractsByConferenceIdAsync(int conferenceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasReviewAsync(int abstractId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextReviewIdAsync(int abstractId)
        {
            throw new NotImplementedException();
        }

        public Task<Abstract> GetById(int abstractId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetExistingReviewAsync(int abstractId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
