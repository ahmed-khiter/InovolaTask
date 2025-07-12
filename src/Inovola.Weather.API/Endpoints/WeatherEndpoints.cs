using Inovola.Weather.Application.Interfaces;
using Inovola.Weather.Domain.Entities;

namespace Inovola.Weather.API.Endpoints
{
    public static class WeatherEndpoints
    {
        public static void MapWeatherEndpoints(this WebApplication app)
        {
            app.MapGet("/api/weather", async (string city, IWeatherService weather) =>
            {
                var forecast = await weather.GetWeatherAsync(city);
                return Results.Ok(forecast);
            })
            .RequireAuthorization()
            .WithName("GetWeatherByCity")
            .WithTags("Weather")
            .WithSummary("Get weather forecast by city")
            .WithDescription("Returns a mock weather forecast for a given city. Requires JWT authentication.")
            .Produces<WeatherForecast>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
