using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using OptionsExample.Models;
using OptionsExample.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherApiSettings>(builder.Configuration.GetSection(WeatherApiSettings.WeatherApi));

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", async (IHttpClientFactory httpClientFactory, IOptions<WeatherApiSettings> opt) =>
{
    WeatherApiSettings settings = opt.Value;

    HttpClient httpClient = httpClientFactory.CreateClient();
    Dictionary<string, string?> queryParams = new()
    {
        {"lat", Convert.ToString(settings.Lat)},
        {"lon", Convert.ToString(settings.Lon)},
        {"appid", settings.ApiKey}

    };
    string uri = QueryHelpers.AddQueryString(settings.Endpoint, queryParams);
    HttpResponseMessage response = await httpClient.GetAsync(uri);
    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest();
    }

    WeatherDTO? weather = await response.Content.ReadFromJsonAsync<WeatherDTO>();
    return Results.Ok(new
    {
        weather?.Weather.FirstOrDefault()?.Description
    });
});

app.Run();
