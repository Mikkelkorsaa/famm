using conferencePlannerApi.Repositories.Interfaces;
using conferencePlannerApi.Repositories.LocalImplementations;
using conferencePlannerApi.Services.Interfaces;
using conferencePlannerApi.Services.Implementations;
using conferencePlannerCore.Models;
using conferencePlannerApi.Repositories.Implementations;
using conferencePlannerCore.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepo, MongoDBUserRepo>();
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

var app = builder.Build();

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