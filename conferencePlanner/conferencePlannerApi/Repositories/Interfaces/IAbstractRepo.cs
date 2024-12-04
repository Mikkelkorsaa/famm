
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
	public interface IAbstractRepo
	{
        //input: Byte array and abstract object
        //Saves image to database
        Task UploadImage(byte[] imageBytes, Abstract _abstract);
	}
}
