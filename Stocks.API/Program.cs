using StocksApi.OptionsSetup;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using StocksApi.Serialization;
using StocksApi.ErrorHandling;
using Stocks.Infrastracture.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Stocks.API.Extensions;
using Stocks.Infrastracture.Clients.Stocks;
using Stocks.Infrastracture.SignalR;
using Stocks.Application;
using Stocks.Infrastracture;
using Serilog;

namespace StocksApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter(builder.Configuration));
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString
                    (DateTime.Today.ToString(builder.Configuration["Json:Serializer:DateTimeFormat"]))
            });
        });

        builder.Services
            .AddApplication()
            .AddInfrastracture();

        // Logging
        builder.Host.UseSerilog((context, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration));

        // Problem details
        builder.Services.AddProblemDetails();

        // Jwt configuration
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearerConfiguration(builder.Configuration);

        // Options
        builder.Services.ConfigureOptions<JwtOptionsSetup>();
        builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<StocksClientOptions>(builder.Configuration.GetSection("Finnhub"));

        // Error handling
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapHub<StocksHub>("/stocks-hub");

        app.Run();
    }
}