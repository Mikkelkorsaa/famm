using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using conferencePlannerApp;
using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerApp.Services.LocalImplementations;
using conferencePlannerApp.Services.Implementations;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, LocalStorageAuthService>();
builder.Services.AddScoped<IUserService, LocalUserService>();
builder.Services.AddScoped<IUploadFileService, LocalUploadFileService>();
builder.Services.AddScoped<IConferenceHandler, LocalConferenceHandler>();
builder.Services.AddScoped<IAbstractService, LocalStorageAbstractService>();
builder.Services.AddScoped<IApiAddressService, ApiAddressService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped(sp =>
{
    string baseAddress;
    if (builder.HostEnvironment.IsDevelopment())
    {
        baseAddress = "https://localhost:7000";
    }
    else
    {
        baseAddress = "https://conferenceplanner-api-dev-euhdb7g8cxceg8ax.westeurope-01.azurewebsites.net"
            ?? throw new InvalidOperationException("API_BASE_URL environment variable not configured");
    }
    Console.WriteLine($"API_BASE_URL: {baseAddress}");
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

await builder.Build().RunAsync();