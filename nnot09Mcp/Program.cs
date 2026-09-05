using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting.Internal;
using nnot09Mcp;
using nnot09Mcp.Database;
using nnot09Mcp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("mcp-data-controller");
builder.Services.AddDbContext<TestContext>();

var app = builder.Build();

app.UseExceptionHandler();

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
