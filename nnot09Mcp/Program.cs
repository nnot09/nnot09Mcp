using Microsoft.Extensions.AI;
using nnot09Mcp.Database;
using nnot09Mcp.Models;
using nnot09Mcp.Repositories;
using nnot09Mcp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Services.AddDbContext<TestContext>();
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<TestService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TestContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapMcp();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
