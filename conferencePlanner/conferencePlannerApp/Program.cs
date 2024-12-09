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
    var baseAddress = builder.HostEnvironment.IsDevelopment() 
        ? builder.Configuration["ApiBaseAddress:Local"] 
        : builder.Configuration["ApiBaseAddress:Production"];
    
    if (string.IsNullOrEmpty(baseAddress))
        throw new InvalidOperationException("API base address not configured");
        
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});


await builder.Build().RunAsync();