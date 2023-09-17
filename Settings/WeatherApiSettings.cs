using System.ComponentModel.DataAnnotations;

namespace OptionsExample.Settings;

public class WeatherApiSettings
{
    public const string WeatherApi = nameof(WeatherApi);

    [Required]
    [Url]
    public string Endpoint { get; set; } = null!;

    [Required]
    public string ApiKey { get; set; } = null!;

    [Required]
    public float Lat { get; set; }

    [Required]
    public float Lon { get; set; }
}