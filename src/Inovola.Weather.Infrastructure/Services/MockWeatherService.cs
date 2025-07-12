using Inovola.Weather.Application.Interfaces;
using Inovola.Weather.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovola.Weather.Infrastructure.Services
{
    public class MockWeatherService(IMemoryCache _cache, IAppLogger _logger, IWeatherRepository _repository) : IWeatherService
    {

        public Task<WeatherForecast> GetWeatherAsync(string city)
        {
            if (_cache.TryGetValue(city, out WeatherForecast cached))
            {
                _logger.LogInfo("Cache hit for city: {City}", city);
                return Task.FromResult(cached);
            }

            var result = _repository.GenerateForecast(city);
            _logger.LogInfo("Cache miss for city: {City}. Generating forecast.", city);
            _cache.Set(city, result, TimeSpan.FromMinutes(5));
            return Task.FromResult(result);
        }

    }
}
