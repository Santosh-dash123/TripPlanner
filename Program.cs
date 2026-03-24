using Microsoft.EntityFrameworkCore;
using TripPlanner.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o =>
    {
        o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        o.CommandTimeout(300);
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrEmpty(port))
{
    app.Run($"http://0.0.0.0:{port}");
}
else
{
    app.Run();
}