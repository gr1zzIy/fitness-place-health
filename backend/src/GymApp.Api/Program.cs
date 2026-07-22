using GymApp.Infrastructure;
using GymApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDatabaseInfrastructure(
    builder.Configuration);

builder.Services.AddIdentityInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.SeedIdentityDataAsync();
}

app.UseHttpsRedirection();

app.Run();