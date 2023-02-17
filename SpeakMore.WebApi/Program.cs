using Microsoft.OpenApi.Models;
using Serilog;
using SpeakMore.Application.Features.CalculateCallValue.DependencyInjection;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.DependencyInjection;
using SpeakMore.Application.Shared.Initializers;
using SpeakMore.Application.Shared.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File($"./logs/SpeakMore.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCalculateCallValueExtensions();
builder.Services.AddServiceExtensions();
builder.Services.AddDataBaseExtensions();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SpeakMore.Api", Version = "v1" });

});

using var scope = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
DbInitializer.Initialize(scope);

var app = builder.Build();

app.UseHsts();


app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
