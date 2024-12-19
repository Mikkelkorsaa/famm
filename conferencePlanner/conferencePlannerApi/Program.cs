using conferencePlannerApi.Repositories.Interfaces; 
using conferencePlannerApi.Repositories.LocalImplementations; 
using conferencePlannerApi.Repositories.Implementations; 
using conferencePlannerCore.Configurations; 
using conferencePlannerApi.Services.Interfaces;
using conferencePlannerApi.Services.Implementations; 
using Microsoft.Extensions.FileProviders; 

// Create a builder for the web application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Add controllers for API endpoints
builder.Services.AddEndpointsApiExplorer(); // Enable API endpoint exploration
builder.Services.AddSwaggerGen(); // Enable Swagger for API documentation

// Register repository dependencies with dependency injection
builder.Services.AddSingleton<IUserRepo, MongoDBUserRepo>(); 
builder.Services.AddSingleton<IConferenceRepo, MongoDBConferenceRepo>(); 
builder.Services.AddSingleton<IAbstractRepo, MongoDBAbstractRepo>(); 
builder.Services.AddSingleton<IVenueRepo, MongoDBVenueRepo>(); 
builder.Services.AddScoped<IEmailService, EmailService>(); 

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = "mongodb+srv://mikkelkorsaa:oCkPG6nncfSz1675@conferenceplanner.pufil.mongodb.net/"; 
});

// Configure email settings
builder.Services.Configure<EmailConfiguration>(options =>
{
    options.SmtpServer = "smtp.gmail.com"; // Set SMTP server address
    options.SmtpPort = 587; // Set SMTP server port
    options.SmtpUsername = "danvafamm@gmail.com"; // Set email username
    options.SmtpPassword = "kenv fqrr cabf qkek"; // Set email password
    options.FromEmail = "danvafamm@gmail.com"; // Set sender email address
    options.FromName = "Danva Famm"; // Set sender name
    options.UseSSL = true; // Enable SSL for email communication
});

// Configure the CORS policy to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add HttpContextAccessor to access HTTP context in services
builder.Services.AddHttpContextAccessor();

// Build the web application
var app = builder.Build();

// Define the uploads directory path
var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
if (!Directory.Exists(uploadsDir)) // Create the directory if it doesn't exist
{
    Directory.CreateDirectory(uploadsDir);
}

// Configure static file serving for the uploads directory
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDir), 
    RequestPath = "/uploads" 
});

// Enable the configured CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection(); 
app.UseAuthentication(); 
app.UseAuthorization(); 
app.MapControllers(); 

app.Run();
