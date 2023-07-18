using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using LottoApi.Models;
using LottoApi.Data;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));

// Add MongoDB client and database
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<MongoDbContext>();

// Retrieve collection names
var mongoDBSettings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
var collectionNames = mongoDBSettings.CollectionNames;

// Register collections in the dependency injection container
foreach (var collectionName in collectionNames)
{
    if (collectionName == "Users")
    {
        builder.Services.AddScoped(serviceProvider =>
    {
        var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
        return mongoDbContext.GetCollection<Users>(collectionName);
    });
    }
    else if (collectionName == "LottoNumbers")
    {
        builder.Services.AddScoped(serviceProvider =>
        {
            var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
            return mongoDbContext.GetCollection<LottoNumbers>(collectionName);
        });
    }
    else if (collectionName == "TicketNumbers")
    {
        builder.Services.AddScoped(serviceProvider =>
        {
            var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
            return mongoDbContext.GetCollection<TicketNumbers>(collectionName);
        });
    } else {
        throw new InvalidOperationException($"Invalid collection name: {collectionName}");
    }
    

}

// Other service registrations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Exception handling
app.UseExceptionHandler("/error"); // Custom error handling endpoint

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
