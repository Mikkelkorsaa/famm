using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using conferencePlannerApp;
using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerApp.Services.LocalImplementations;
using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, LocalStorageAuthService>();
builder.Services.AddScoped<IUserService, LocalUserService>();
builder.Services.AddScoped<IUploadFileService, LocalUploadFileService>();
builder.Services.AddScoped<IConferenceHandler, LocalConferenceHandler>();
builder.Services.AddScoped<IAbstractService, LocalAbstractService>();

await builder.Build().RunAsync();