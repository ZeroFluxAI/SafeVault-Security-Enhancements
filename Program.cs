using SafeVault.Data;
using SafeVault.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

DatabaseInitializer.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SafeVault API v1");
        c.RoutePrefix = string.Empty; // Serves Swagger UI at root (http://localhost:5234/)
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
