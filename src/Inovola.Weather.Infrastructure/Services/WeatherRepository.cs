using Inovola.Weather.Application.Interfaces;
using Inovola.Weather.Domain.Entities;

namespace Inovola.Weather.Infrastructure.Services
{
    public class WeatherRepository : IWeatherRepository
    {
        public WeatherForecast GenerateForecast(string city)
        {
            return new WeatherForecast
            {
                City = city,
                Summary = "Sunny",
                TemperatureC = new Random().Next(20, 40)
            };
        }
    }
}
