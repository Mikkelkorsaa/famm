
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
	public interface IAbstractRepo
	{
        //input: Image file and abstract object
        //Saves image to database
        Task UploadImage(byte[] imageBytes, Abstract _abstract);
	}
}
