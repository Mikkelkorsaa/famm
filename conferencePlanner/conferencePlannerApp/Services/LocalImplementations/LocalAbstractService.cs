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
        Id = 2,
        ConferenceId = 102,
        SenderName = "Alice Green",
        PresenterEmail = "alice.green@example.com",
        CoAuthors = new List<string> { "Tom White", "Lucy Brown" },
        Organization = "HealthTech Institute",
        Title = "Advances in Telemedicine",
        KeyValues = "Telemedicine, Healthcare, Innovation",
                AbstractText = "In the heart of a sprawling city, the contrast between the quiet, timeless elegance of the park and the bustling energy of the streets beyond was striking. The park, with its winding paths, ancient trees, and serene lakes, stood as an oasis of peace. It was here that people came to escape, if only for a moment, from the fast-paced world that often seemed to rush ahead without pause. The tall, stone walls of the city surrounded the green expanse, reminding anyone who ventured close that this tranquility existed only within the confines of this oasis.\r\n\r\nThe seasons played their roles with perfect choreography, each bringing a new layer of beauty. In the spring, the cherry blossoms would bloom in delicate pink and white hues, carpeting the ground in soft petals. Summer brought warmth and lush green, with families and friends gathering for picnics, or simply sitting by the water to enjoy the sunshine. As autumn arrived, the trees transformed, their leaves turning golden, orange, and red, painting the landscape with vibrant colors. Winter, while quieter, was just as enchanting. The snow would cover the ground, creating a quiet stillness that was both peaceful and magical.\r\n\r\nEach day, people from all walks of life came and went. Joggers and dog walkers, tourists marveling at the beauty, and locals seeking solace from the chaos of the city. Some sat on benches, gazing at the horizon, lost in their thoughts. Others brought books, losing themselves in the written word while the world carried on around them. It was a place of reflection, of connection to nature, and a reminder that in the midst of the rush of life, moments of calm were still possible. The park, in its timeless beauty, held secrets of the city that only those who took the time to stop and listen could truly hear.",

        Category = "Healthcare",
        Reviews = new List<Review>
        {
            new Review
            {
                Id = 3,
                UserId = 789,
                Comment = "Well-researched and engaging.",
                Criterias = new List<Criteria>
                {
                    new Criteria { Name = "Content Quality", Grade = 4 },
                    new Criteria { Name = "Speaker Performance", Grade = 5 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 }
                }
            }
        }
    },
    new Abstract
    {
        Id = 3,
        ConferenceId = 103,
        SenderName = "Mark Black",
        PresenterEmail = "mark.black@example.com",
        CoAuthors = new List<string> { "Emily Davis" },
        Organization = "EcoTech Solutions",
        Title = "Green Energy Trends",
        KeyValues = "Green Energy, Sustainability, Innovation",
        AbstractText = "An overview of current trends in green energy technology.",
        Category = "Environment",
        Reviews = new List<Review>
        {
            new Review
            {
                Id = 4,
                UserId = 111,
                Comment = "Very inspiring work on sustainability.",
                Criterias = new List<Criteria>
                {
                    new Criteria { Name = "Content Quality", Grade = 5 },
                    new Criteria { Name = "Speaker Performance", Grade = 4 },
                    new Criteria { Name = "Organization", Grade = 4 }
                }
            }
        }
    },
    new Abstract
    {
        Id = 4,
        ConferenceId = 104,
        SenderName = "Sophie Brown",
        PresenterEmail = "sophie.brown@example.com",
        CoAuthors = new List<string> { "Chris Johnson" },
        Organization = "AI Innovations Inc.",
        Title = "Ethics in Artificial Intelligence",
        KeyValues = "AI, Ethics, Responsibility",
        AbstractText = "Discusses ethical considerations in AI development.",
        Category = "Technology",
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 5,
        ConferenceId = 105,
        SenderName = "Jack White",
        PresenterEmail = "jack.white@example.com",
        CoAuthors = new List<string> { },
        Organization = "EduTech Labs",
        Title = "Gamification in Education",
        KeyValues = "Education, Gamification, Technology",
        AbstractText = "Explores the impact of gamification in modern education.",
        Category = "Education",
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 6,
        ConferenceId = 106,
        SenderName = "Nancy Adams",
        PresenterEmail = "nancy.adams@example.com",
        CoAuthors = new List<string> { "Henry Taylor" },
        Organization = "AgriTech Inc.",
        Title = "Smart Farming Solutions",
        KeyValues = "Agriculture, Technology, Innovation",
        AbstractText = "Innovative technologies in smart farming and their benefits.",
        Category = "Agriculture",
        
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 7,
        ConferenceId = 107,
        SenderName = "James Wilson",
        PresenterEmail = "james.wilson@example.com",
        CoAuthors = new List<string> { },
        Organization = "AutoTech Research",
        Title = "Future of Autonomous Vehicles",
        KeyValues = "Autonomous Vehicles, AI, Transportation",
        AbstractText = "Examines the challenges and opportunities in autonomous vehicle adoption.",
        Category = "Transportation",
        
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 8,
        ConferenceId = 108,
        SenderName = "Emma Stone",
        PresenterEmail = "emma.stone@example.com",
        CoAuthors = new List<string> { },
        Organization = "BioHealth Group",
        Title = "Biotechnology Breakthroughs",
        KeyValues = "Biotechnology, Healthcare, Innovation",
        AbstractText = "Recent breakthroughs in biotechnology and their implications.",
        Category = "Healthcare",
        
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 9,
        ConferenceId = 109,
        SenderName = "Oliver Green",
        PresenterEmail = "oliver.green@example.com",
        CoAuthors = new List<string> { },
        Organization = "FinTech Innovators",
        Title = "Blockchain in Finance",
        KeyValues = "Blockchain, Finance, Technology",
        AbstractText = "Exploring the applications of blockchain in financial systems.",
        Category = "Finance",
        
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 10,
        ConferenceId = 110,
        SenderName = "Isabella Clark",
        PresenterEmail = "isabella.clark@example.com",
        CoAuthors = new List<string> { "Ethan Martin" },
        Organization = "CleanEnergy Initiative",
        Title = "Solar Power Innovations",
        KeyValues = "Solar Power, Clean Energy, Sustainability",
        AbstractText = "Innovations in solar power and their role in a sustainable future.",
        Category = "Environment",
        
        Reviews = new List<Review>()
    },
    new Abstract
    {
        Id = 11,
        ConferenceId = 111,
        SenderName = "Liam Johnson",
        PresenterEmail = "liam.johnson@example.com",
        CoAuthors = new List<string> { },
        Organization = "SpaceTech Solutions",
        Title = "Exploring Mars: Challenges and Opportunities",
        KeyValues = "Mars, Space Exploration, Technology",
        AbstractText = "In the heart of a sprawling city, the contrast between the quiet, timeless elegance of the park and the bustling energy of the streets beyond was striking. The park, with its winding paths, ancient trees, and serene lakes, stood as an oasis of peace. It was here that people came to escape, if only for a moment, from the fast-paced world that often seemed to rush ahead without pause. The tall, stone walls of the city surrounded the green expanse, reminding anyone who ventured close that this tranquility existed only within the confines of this oasis.\r\n\r\nThe seasons played their roles with perfect choreography, each bringing a new layer of beauty. In the spring, the cherry blossoms would bloom in delicate pink and white hues, carpeting the ground in soft petals. Summer brought warmth and lush green, with families and friends gathering for picnics, or simply sitting by the water to enjoy the sunshine. As autumn arrived, the trees transformed, their leaves turning golden, orange, and red, painting the landscape with vibrant colors. Winter, while quieter, was just as enchanting. The snow would cover the ground, creating a quiet stillness that was both peaceful and magical.\r\n\r\nEach day, people from all walks of life came and went. Joggers and dog walkers, tourists marveling at the beauty, and locals seeking solace from the chaos of the city. Some sat on benches, gazing at the horizon, lost in their thoughts. Others brought books, losing themselves in the written word while the world carried on around them. It was a place of reflection, of connection to nature, and a reminder that in the midst of the rush of life, moments of calm were still possible. The park, in its timeless beauty, held secrets of the city that only those who took the time to stop and listen could truly hear.",
        Category = "Space Exploration",
        
        Reviews = new List<Review>()
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
