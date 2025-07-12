namespace Inovola.Weather.Tests
{
    public class InMemoryAuthServiceTests
    {
        private readonly Mock<IJwtTokenGenerator> _jwtMock = new();
        private readonly Mock<IAppLogger> _loggerMock = new();

        [Fact]
        public void Register_ShouldReturnToken_WhenUserIsNew()
        {
            // Arrange
            var service = new InMemoryAuthService(_jwtMock.Object, _loggerMock.Object);
            _jwtMock.Setup(x => x.GenerateToken("ahmed")).Returns("mocked-jwt");

            // Act
            var result = service.Register("ahmed", "123456");

            // Assert
            result.Should().Be("mocked-jwt");
            _loggerMock.Verify(x => x.LogInfo("User registered: {Username}", "ahmed"), Times.Once);
        }

        [Fact]
        public void Register_ShouldThrow_WhenUserAlreadyExists()
        {
            var service = new InMemoryAuthService(_jwtMock.Object, _loggerMock.Object);
            service.Register("user1", "pass");

            var act = () => service.Register("user1", "newpass");

            act.Should().Throw<Exception>().WithMessage("User already exists");
        }

        [Fact]
        public void Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var service = new InMemoryAuthService(_jwtMock.Object, _loggerMock.Object);
            service.Register("ahmed", "123456");
            _jwtMock.Setup(x => x.GenerateToken("ahmed")).Returns("valid-jwt");

            var result = service.Login("ahmed", "123456");

            result.Should().Be("valid-jwt");
        }

        [Fact]
        public void Login_ShouldThrow_WhenCredentialsAreInvalid()
        {
            var service = new InMemoryAuthService(_jwtMock.Object, _loggerMock.Object);
            var act = () => service.Login("unknown", "wrong");

            act.Should().Throw<Exception>().WithMessage("Invalid credentials");
        }
    }
}
