using System.Text.Json.Serialization;

namespace OptionsExample.Models;

public class WeatherDTO
{
    public List<Weather> Weather { get; set; } = new();
}

public class Weather
{
    public string Description { get; set; } = null!;
}