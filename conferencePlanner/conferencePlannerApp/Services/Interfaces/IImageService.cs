using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;

namespace conferencePlannerApp.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Uploads an image file to the server
        /// </summary>
        /// <param name="file">The image file to upload</param>
        /// <returns>Upload result containing file name and metadata</returns>
        Task<ImageUploadResult> UploadImageAsync(IBrowserFile file);

        /// <summary>
        /// Retrieves an image from the server by its filename
        /// </summary>
        /// <param name="fileName">Name of the file to retrieve</param>
        /// <returns>Image data as byte array</returns>
        Task<byte[]> GetImageAsync(string fileName);

        /// <summary>
        /// Deletes an image from the server
        /// </summary>
        /// <param name="fileName">Name of the file to delete</param>
        Task DeleteImageAsync(string fileName);
    }
}