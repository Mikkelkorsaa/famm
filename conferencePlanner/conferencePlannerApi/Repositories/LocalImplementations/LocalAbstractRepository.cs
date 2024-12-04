using conferencePlannerApi.Repositories.Interfaces;

namespace conferencePlannerApi.Repositories.LocalImplementations
{
	public class localAbstractRepository : IAbstractRepository
	{
		public Task UploadImage(byte[] imageBytes)
		{
			throw new NotImplementedException();
		}
	}
}
