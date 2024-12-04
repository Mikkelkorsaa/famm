
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.Interfaces
{
	public interface IAbstractRepo
	{
		Task UploadImage(byte[] imageBytes, Abstract _abstract);
	}
}
