using Npgsql;
using ShopItemAPI.Interfaces;
using ShopItemAPI.Repositories;
using ShopItemAPI.Services;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopItemAPI.Data;
using DbUp;
using System.Reflection;
using Serilog;
using ShopItemAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IShopItemRepository, ShopItemRepository>();
builder.Services.AddTransient<IShopItemService, ShopItemService>();

string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");

var upgrader =
        DeployChanges.To
            .PostgresqlDatabase(dbConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

var result = upgrader.PerformUpgrade();

builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

builder.Services.AddAutoMapper(typeof(Program));

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

app.UseMiddleware<Middleware>();

app.Run();
