using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LottoApi.Models;
using LottoApi.Data;
using MongoDBService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MongoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MongoDbContext") ?? throw new InvalidOperationException("Connection string 'MongoDbContext' not found.")));

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
