using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerApi.Repositories.LocalImplementations;
using conferencePlannerApi.Repositories.Implementations;
using conferencePlannerCore.Configurations;
using conferencePlannerApi.Services.Interfaces;
using conferencePlannerApi.Services.Implementations;
using conferencePlannerCore.Configurations;
using Microsoft.Extensions.FileProviders;
using conferencePlannerApi.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepo, MongoDBUserRepo>();
builder.Services.AddSingleton<IConferenceRepo, MongoDBConferenceRepo>();
builder.Services.AddSingleton<IAbstractRepo, MongoDBAbstractRepo>();
builder.Services.AddSingleton<IVenueRepo, LocalVenueRepo>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = "mongodb+srv://mikkelkorsaa:oCkPG6nncfSz1675@conferenceplanner.pufil.mongodb.net/";
});

builder.Services.Configure<EmailConfiguration>(options =>
{
    options.SmtpServer = "smtp.gmail.com";
    options.SmtpPort = 587;
    options.SmtpUsername = "danvafamm@gmail.com";
    options.SmtpPassword = "kenv fqrr cabf qkek";
    options.FromEmail = "danvafamm@gmail.com";
    options.FromName = "Danva Famm";
    options.UseSSL = true;
});

// Configure the CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
if (!Directory.Exists(uploadsDir))
{
    Directory.CreateDirectory(uploadsDir);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDir),
    RequestPath = "/uploads"
});

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
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