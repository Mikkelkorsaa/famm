using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace conferencePlannerApp.Services.Interfaces;

public interface IUploadImageService
{
    //input: Image file and abstract object
    //Saves image to database
    Task UploadImage(IBrowserFile file, Abstract _abstract);
}
