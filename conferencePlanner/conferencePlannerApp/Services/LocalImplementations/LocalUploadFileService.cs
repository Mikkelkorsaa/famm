using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;


namespace conferencePlannerApp.Services.LocalImplementations;

public class LocalUploadFileService : IUploadFileService
{
    public async Task<string> ConvertToBase64String(IBrowserFile file)
    {
        var byteArray = await ConvertToByteArray(file);
		string base64string = Convert.ToBase64String(byteArray);
		return base64string;
    }

	public async Task<byte[]> ConvertToByteArray(IBrowserFile file)
	{
		if (file == null || file.Size == 0) throw new Exception("File is empty");
		using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
		using var ms = new MemoryStream();
		await stream.CopyToAsync(ms);
		byte[] byteArray = ms.ToArray();

		return byteArray;
	}
}
 