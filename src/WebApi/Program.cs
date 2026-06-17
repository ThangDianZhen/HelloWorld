using Application.Interfaces.Persistence.Repositories;
using Application.Interfaces.Services.Entities;
using Application.Services.Entities;
using Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Persistence
builder.Services.AddSingleton<IGreetingRepository, GreetingRepository>();

// Application services
builder.Services.AddScoped<IGreetingService, GreetingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapControllers();

app.Run();
