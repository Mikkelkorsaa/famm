using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Uploads an image to the server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ImageUploadResult> UploadImageAsync(IBrowserFile file);

        /// <summary>
        /// Gets an image from the server
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<ImageResponse> GetImageAsync(string fileName);

        /// <summary>
        /// Deletes an image from the server
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task DeleteImageAsync(string fileName);
    }
}