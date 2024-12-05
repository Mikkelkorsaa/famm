using conferencePlannerApp.Services.Interfaces;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;


namespace conferencePlannerApp.Services.LocalImplementations;

public class LocalUploadFileService : IUploadFileService
{
    public async Task<byte[]> ConvertToBase64String(IBrowserFile file)
    {
        if (file == null || file.Size == 0) return null;

        using var stream = file.OpenReadStream(maxAllowedSize: 16 * 1024 * 1024);
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        byte[] byteArray = ms.ToArray();

        return byteArray;
    }

    public Task<string> ConvertToImageUrl(byte[] bytearr)
    {
        throw new NotImplementedException();
    }


}
