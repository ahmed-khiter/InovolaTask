using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovola.Weather.Domain.Entities
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public string Summary { get; set; }
        public float TemperatureC { get; set; }
    }
}
