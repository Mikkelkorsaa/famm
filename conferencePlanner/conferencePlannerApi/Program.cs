using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerApi.Repositories.LocalImplementations;
using conferencePlannerApi.Services.Interfaces;
using conferencePlannerApi.Services.Implementations;
using conferencePlannerCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection for Repositories
builder.Services.AddSingleton<IUserRepo, LocalUserRepo>();
builder.Services.AddSingleton<IConferenceRepo, LocalConferenceRepo>();
builder.Services.AddSingleton<IAbstractRepo, LocalAbstractRepo>();

// Configuration for Email Service
builder.Services.Configure<EmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailService, EmailService>();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Application configuration
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
Console.WriteLine($"Configured API Base URL: {apiBaseUrl}");

var app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
