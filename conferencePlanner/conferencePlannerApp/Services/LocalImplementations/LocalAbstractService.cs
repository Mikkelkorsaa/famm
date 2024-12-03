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
        Picture = new byte[] { } // Add byte data for the picture if needed
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
        Picture = new byte[] { } // Add byte data for the picture if needed
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
    }
}
