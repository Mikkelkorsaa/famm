using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.Interfaces;

public interface IUploadImageService
{
	Task UploadImage(IBrowserFile file, Abstract _abstract);
	Task<string> GetImageUrl(Abstract _abstract);
}
