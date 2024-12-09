using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.Interfaces;

public interface IUploadFileService
{
    //input: IBrowserfile that is either a png, jpg, or jpeg
    //Manipulation: Converts IBrowserFile to a base64string
    //output: base64string
    Task<string> ConvertToBase64String(IBrowserFile file);

	//input: IBrowserfile that is either a png, jpg, or jpeg
	//Manipulation: Converts IBrowserFile to a byte array
	//output: byte array
	Task<byte[]> ConvertToByteArray(IBrowserFile file);

}
