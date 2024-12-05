
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
	public interface IAbstractService
	{
		Task AddAbstract(Abstract _abstract);
		Task<List<Abstract>> GetAllAbstracts();

		Task UpdateAbstract (Abstract _abstract);
    }
}
