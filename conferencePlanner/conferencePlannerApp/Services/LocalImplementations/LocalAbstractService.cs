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
	}
}
