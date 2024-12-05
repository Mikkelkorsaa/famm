using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.Interfaces;

public interface IUploadFileService
{
    //input: IBrowserfile that is either a png, jpg, or jpeg
    //Manipulation: Converts IBrowserFile to a byte[]
    //output: byte[]
    Task<byte[]> ConvertToBase64String(IBrowserFile file);

    //input: byte[] 
    //Manipulation: Converts byte[] to imageurl
    //output: String
    Task<string> ConvertToImageUrl(byte[] bytearr);
}
