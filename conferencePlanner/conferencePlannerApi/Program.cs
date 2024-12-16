using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerApi.Repositories.LocalImplementations;
using conferencePlannerApi.Services.Interfaces;
using conferencePlannerApi.Services.Implementations;
using conferencePlannerCore.Configurations;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepo, LocalUserRepo>();
builder.Services.AddSingleton<IConferenceRepo, LocalConferenceRepo>();
builder.Services.AddSingleton<IAbstractRepo, LocalAbstractRepo>();

builder.Services.Configure<EmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailService, EmailService>();

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