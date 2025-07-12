using Inovola.Weather.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inovola.Weather.Infrastructure.Services
{
    public class InMemoryAuthService(IJwtTokenGenerator _jwt, IAppLogger _logger) : IAuthService
    {
        private static readonly ConcurrentDictionary<string, string> _users = new();

        public string Register(string username, string password)
        {
            if (!_users.TryAdd(username, password))
            {
                _logger.LogWarning("User already exists: {Username}", username);
                throw new Exception("User already exists");
            }
            _logger.LogInfo("User registered: {Username}", username);
            return _jwt.GenerateToken(username);
        }

        public string Login(string username, string password)
        {
            if (_users.TryGetValue(username, out var pass) && pass == password)
            {
                _logger.LogInfo("User logged in: {Username}", username);
                return _jwt.GenerateToken(username);
            }
            _logger.LogWarning("Invalid login attempt: {Username}", username);
            throw new Exception("Invalid credentials");
        }
    }
}
