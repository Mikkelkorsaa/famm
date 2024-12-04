using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.LocalImplementations
{
	public class LocalUploadImageService : IUploadImageService
	{
		public Task UploadImage(IBrowserFile file, Abstract _abstract)
		{
			if (file is not null) {
                Console.WriteLine("Uploading file...");
				return Task.CompletedTask;
			}
			else
			{
				Console.WriteLine("No file to upload");
				return Task.CompletedTask;
			}
		}
	}
}
