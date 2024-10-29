using Microsoft.EntityFrameworkCore;
using StreetHub.Repositories;
using StreetHub.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite;
using StreetHub.Data;

// Create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Check if the application is running in Docker
string dockerEnv = Environment.GetEnvironmentVariable("RUNNING_IN_DOCKER") ?? string.Empty;
bool isRunningInDocker = !string.IsNullOrEmpty(dockerEnv) && Convert.ToBoolean(dockerEnv);

// Set database connection parameters based on the environment
string host = isRunningInDocker ? "postgres" : "localhost";
string database = "streetdb";
string username = "postgres";
string password = "yourpassword"; // Consider using a more secure method for managing passwords
string port = "5432";

// Construct the PostgreSQL connection string
var connectionString = $"Host={host};Database={database};Username={username};Password={password};Port={port};";

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Add geometry converter for handling spatial data
        options.SerializerSettings.Converters.Add(new NetTopologySuite.IO.Converters.GeometryConverter());
    });

// Configure the DbContext with PostgreSQL and NetTopologySuite support
builder.Services.AddDbContext<StreetContext>(options =>
    options.UseNpgsql(connectionString, o => o.UseNetTopologySuite()));

// Register repositories and services for dependency injection
builder.Services.AddScoped<IStreetRepository, StreetRepository>();
builder.Services.AddScoped<IStreetService, StreetService>();

// Set up Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map the controllers to the endpoints

app.Run(); // Run the application
