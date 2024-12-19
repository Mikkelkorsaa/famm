using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.Interfaces;

public interface IUploadFileService
{
    /// <summary>
    /// Converts an image file to a base64 string
    /// </summary>
    /// <param name="file">The image file to be converted (png, jpg, or jpeg)</param>
    /// <returns>The base64 string representation of the image file</returns>
    Task<string> ConvertToBase64String(IBrowserFile file);

    /// <summary>
    /// Converts an image file to a byte array
    /// </summary>
    /// <param name="file">The image file to be converted (png, jpg, or jpeg)</param>
    /// <returns>The byte array representation of the image file</returns>
    Task<byte[]> ConvertToByteArray(IBrowserFile file);
}

