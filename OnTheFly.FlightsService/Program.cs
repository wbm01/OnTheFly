using Microsoft.Extensions.Options;
using OnTheFly.FlightsService.config;
using OnTheFly.FlightsService.Repositories;
using OnTheFly.FlightsService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection("MongoDBConfig"));

builder.Services.AddSingleton<IMongoDBConfig>(s => s.GetRequiredService<IOptions<MongoDBConfig>>().Value);
builder.Services.AddSingleton<FlightsRepository>();
builder.Services.AddSingleton<FlightService>();

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
