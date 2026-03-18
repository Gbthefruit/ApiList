using ApiList.Context;
using ApiList.Extensions;
using ApiList.Filters;
using ApiList.Logging;
using ApiList.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => {

    options.Filters.Add(typeof(ApiExceptionFilter));

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

string? mySqlConnection = builder.Configuration.GetConnectionString("TarefasConnection");

builder.Services.AddDbContext<TarefaDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddScoped<IProgressoRepository, ProgressoRepository>();
builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration {

    LogLevel = LogLevel.Information

}));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ConfigureExceptionHandler();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();