using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using conferencePlannerApp;
using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerApp.Services.LocalImplementations;

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

builder.Services.AddScoped(sp =>
{
    string baseAddress;
    if (builder.HostEnvironment.IsDevelopment())
    {
        baseAddress = "https://localhost:7000";
    }
    else
    {
        baseAddress = builder.Configuration.GetValue<string>("API_BASE_URL") 
            ?? throw new InvalidOperationException("API_BASE_URL environment variable not configured");
    }
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

Console.WriteLine($"Environment: {builder.HostEnvironment.Environment}");
Console.WriteLine($"API_BASE_URL: {Environment.GetEnvironmentVariable("API_BASE_URL")}");


await builder.Build().RunAsync();