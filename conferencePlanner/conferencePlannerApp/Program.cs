using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using conferencePlannerApp;
using Blazored.LocalStorage;
using conferencePlannerApp.Services.Interfaces;
using conferencePlannerApp.Services.LocalImplementations;
using conferencePlannerApp.Services.Implementations;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using conferencePlannerCore.Models;
using Microsoft.AspNetCore.Components.Authorization;
using conferencePlannerApp.Services.RoleAutherization;

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


await builder.Build().RunAsync();