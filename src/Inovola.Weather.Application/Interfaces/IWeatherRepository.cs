using Inovola.Weather.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovola.Weather.Application.Interfaces
{
    public interface IWeatherRepository
    {
        WeatherForecast GenerateForecast(string city);
    }
}
