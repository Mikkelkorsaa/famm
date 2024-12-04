using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerCore.Models;

namespace conferencePlannerApi.Repositories.LocalImplementations
{
	public class LocalAbstractRepo : IAbstractRepo
	{
		public Task UploadImage(byte[] imageBytes, Abstract _abstract)
		{
			if (imageBytes == null)
			{
                Console.WriteLine("No img to be uploaded");
				return Task.CompletedTask;
			}

			Console.WriteLine("Uploading image to local storage");
			return Task.CompletedTask;
		}
	}
}
