using conferencePlannerApp.Services.LocalImplementations;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Blazored.LocalStorage;

namespace conferencePlannerApp.Services.LocalImplementations
{
    public class LocalStorageAbstractService : IAbstractService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "localAbstrsactList";
        List<Abstract> _abstracts = new List<Abstract>();

		public LocalStorageAbstractService(ILocalStorageService localStorage)
		{
			_localStorage = localStorage;
		}


		public async Task AddAbstract(Abstract _abstract)
		{
			_abstracts.Add(_abstract);
			await _localStorage.SetItemAsync(StorageKey, _abstract);
		}

		public async Task<List<Abstract>> GetAbstracts() => await _localStorage.GetItemAsync<List<Abstract>>(StorageKey);

		public Task UpdateAbstract(Abstract _abstract)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAbstract(Abstract _abstract)
		{
			throw new NotImplementedException();
		}
	}
}
