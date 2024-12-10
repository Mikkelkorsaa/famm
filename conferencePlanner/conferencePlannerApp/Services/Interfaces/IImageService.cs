using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IBrowserFile file);
        Task<ImageResponse> GetImageAsync(string fileName);
        Task DeleteImageAsync(string fileName);
    }
}