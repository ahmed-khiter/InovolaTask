using Inovola.Weather.Domain.Entities;

namespace Inovola.Weather.Tests
{

    public class MockWeatherServiceTests
    {
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly Mock<IWeatherRepository> _mockRepo;
        private readonly Mock<IAppLogger> _mockLogger;
        private readonly MockWeatherService _service;

        public MockWeatherServiceTests()
        {
            _mockCache = new Mock<IMemoryCache>();
            _mockRepo = new Mock<IWeatherRepository>();
            _mockLogger = new Mock<IAppLogger>();

            _service = new MockWeatherService(_mockCache.Object, _mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsFromCache_WhenCacheHit()
        {
            // Arrange
            var city = "Cairo";
            var expected = new WeatherForecast { City = city, Summary = "Sunny", TemperatureC = 30 };

            var mockEntry = new object();
            _mockCache.Setup(mc => mc.TryGetValue(city, out expected)).Returns(true);

            // Act
            var result = await _service.GetWeatherAsync(city);

            // Assert
            Assert.Equal(expected.City, result.City);
            Assert.Equal(expected.TemperatureC, result.TemperatureC);
            _mockLogger.Verify(l => l.LogInfo("Cache hit for city: {City}", city), Times.Once);
        }

        [Fact]
        public async Task GetWeatherAsync_GeneratesForecast_WhenCacheMiss()
        {
            // Arrange
            var city = "Alex";
            var expected = new WeatherForecast { City = city, Summary = "Sunny", TemperatureC = 27 };

            _mockCache.Setup(mc => mc.TryGetValue(city, out It.Ref<WeatherForecast>.IsAny)).Returns(false);
            _mockRepo.Setup(r => r.GenerateForecast(city)).Returns(expected);

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new MockWeatherService(memoryCache, _mockLogger.Object, _mockRepo.Object);

            // Act
            var result = await service.GetWeatherAsync(city);

            // Assert
            Assert.Equal(expected.City, result.City);
            Assert.Equal(expected.TemperatureC, result.TemperatureC);
            _mockRepo.Verify(r => r.GenerateForecast(city), Times.Once);
        }
    }

}
