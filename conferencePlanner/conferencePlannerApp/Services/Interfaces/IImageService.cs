using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Uploads an image to the server
        /// </summary>
        /// <param name="file">The image file to be uploaded</param>
        /// <returns>The result of the image upload</returns>
        Task<ImageUploadResult> UploadImageAsync(IBrowserFile file);

        /// <summary>
        /// Retrieves an image from the server
        /// </summary>
        /// <param name="fileName">The name of the image file to be retrieved</param>
        /// <returns>The image response containing the image data</returns>
        Task<ImageResponse> GetImageAsync(string fileName);

        /// <summary>
        /// Deletes an image from the server
        /// </summary>
        /// <param name="fileName">The name of the image file to be deleted</param>
        Task DeleteImageAsync(string fileName);
    }
}

