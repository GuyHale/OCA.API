using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Builder;
using System.Data.Entity.Infrastructure;
using OCA.API.Interfaces;
using OCA.API.Services;

var builder = WebApplication.CreateBuilder(args);

AWSOptions aWSOptions = new Amazon.Extensions.NETCore.Setup.AWSOptions()
{
    Region = RegionEndpoint.EUWest2,
    Profile = "guy_hale_legend"
};

// Add services to the container.

builder.Services.AddControllers();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services
    .AddSingleton<IDapperWrapper, DapperWrapper>()
    .AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
    .AddSingleton<ICryptocurrencyRetrieval, CryptocurrencyRetrieval>()
    .AddAWSService<IAmazonDynamoDB>()
    .AddDefaultAWSOptions(aWSOptions)
    .AddSingleton<IDynamoDBContext, DynamoDBContext>()
    .AddScoped<ICustomAuthenticationService, CustomAuthenticationService>()
    .AddScoped<ICryptocurrencyApi, CryptocurrencyApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context =>
        {
            await context.Response.WriteAsync($"{System.Reflection.Assembly.GetExecutingAssembly().GetName()?.Name} running");
        });
    });

await app.RunAsync();
