using GymApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(configuration.GetConnectionString("Database"))
        .UseSnakeCaseNamingConvention()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();