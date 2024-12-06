
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
	public interface IAbstractService
	{
		//Input: Abstract obj.
		//Manipulation: Sends abstract to database
		Task AddAbstract(Abstract _abstract);

		//Output: returns a List of abstract objects
		Task<List<Abstract>> GetAbstracts();

		//Input: Abstract obj.
		//Manipulation: 1. Finds abstract in Db with matching id 2.Updates the existing obj. in the Db with the current obj. from clientside
		Task UpdateAbstract (Abstract _abstract);

		//Input: Abstract obj.
		//Manipulation: Find abstract with matching id in DB and then deletes it
		Task DeleteAbstract (Abstract _abstract);
    }
}
