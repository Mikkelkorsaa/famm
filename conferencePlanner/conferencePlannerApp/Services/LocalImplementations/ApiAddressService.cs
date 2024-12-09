using conferencePlannerApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public class ApiAddressService : IApiAddressService 
{
    private readonly IConfiguration _configuration;
    private readonly IWebAssemblyHostEnvironment _environment;

    public ApiAddressService(IConfiguration configuration, IWebAssemblyHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public string BaseAddress => _environment.IsDevelopment() 
        ? _configuration["ApiBaseAddress:Local"] ?? throw new MissingFieldException("ApiBaseAddress:Local is not set in appsettings.json")
        : _configuration["ApiBaseAddress:Production"] ?? throw new MissingFieldException("ApiBaseAddress:Production is not set in appsettings.json");
}