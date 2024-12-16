using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.LocalImplementations
{
    public class LocalAbstractRepo : IAbstractRepo
    {
        List<Abstract> _abstracts = new()
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
},

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
     Id = 4,
            UserId = 104,
            Criterias = new List<Criteria> { new Criteria { Name = "Relevance"}, new Criteria { Name = "Originality" } }
        },
        new Review
        {
            Id = 4,
            UserId = 104,
            Criterias = new List<Criteria> { new Criteria { Name = "Relevance"}, new Criteria { Name = "Originality" } }

        }
    }
},

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
            UserId = 2,
            Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 3 }, new Criteria { Name = "Originality", Grade = 4 } },
            Comment = "Test112"
        },
        new Review
        {
            Id = 6,
            UserId = 3,
            Criterias = new List<Criteria> { new Criteria { Name = "Relevance", Grade = 4 }, new Criteria { Name = "Originality", Grade = 5 } },
            Comment = "Test"
        }
    }
}};


        private int _lastId = 3;

        public async Task<Abstract> GetByIdAsync(int id)
        {
            var response = _abstracts.FirstOrDefault(a => a.Id == id);
            return await Task.FromResult(response != null ? response : throw new Exception("Abstract not found"));
        }

        public async Task<List<Abstract>> GetAllAsync()
        {
            var result = _abstracts;
            return await Task.FromResult(result.Any() ? result : throw new Exception("No abstracts found"));
        }

        public async Task<Abstract> CreateAsync(Abstract @abstract)
        {
            var response = _abstracts.FirstOrDefault(a => a.Id == @abstract.Id);
            if (response == null)
            {
                var newAbstract = @abstract with { Id = ++_lastId };
                _abstracts.Add(newAbstract);
                return await Task.FromResult(newAbstract);
            }
            else
                throw new Exception("Abstract ID already exists");
        }

        public async Task<Abstract> UpdateAsync(Abstract @abstract)
        {
            var index = _abstracts.FindIndex(a => a.Id == @abstract.Id);
            if (index == -1)
                throw new Exception("Abstract not found");

            var updatedAbstract = @abstract with { Id = _abstracts[index].Id };
            _abstracts[index] = updatedAbstract;
            return await Task.FromResult(updatedAbstract);
        }

        public Task DeleteAsync(int id)
        {
            var index = _abstracts.FindIndex(a => a.Id == id);
            if (index == -1)
                throw new Exception("Abstract not found");
            _abstracts.RemoveAt(index);

            return Task.CompletedTask;
        }

        public async Task<Abstract> UpdateReview(int abstractId, Review review)
        {
            var abstractToUpdate = _abstracts.FirstOrDefault(a => a.Id == abstractId);
            if (abstractToUpdate == null)
                throw new Exception("Abstract not found");

            var reviewIndex = abstractToUpdate.Reviews.FindIndex(r => r.Id == review.Id);
            if (reviewIndex == -1)
                throw new Exception("Review not found");

            abstractToUpdate.Reviews[reviewIndex] = review;
            return await Task.FromResult(abstractToUpdate);
        }
    }
}
