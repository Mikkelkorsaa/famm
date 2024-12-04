
namespace conferencePlannerApi.Repositories.Interfaces
{
	public interface IAbstractRepository
	{
		Task UploadImage(byte[] imageBytes);
	}
}
