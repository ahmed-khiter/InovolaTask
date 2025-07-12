using Inovola.Weather.Application.DTOs;
using Inovola.Weather.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inovola.Weather.API.Endpoints
{
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithDescription("Endpoints for user registration and login.");

            group.MapPost("/register", async ([FromBody] LoginDto dto, IAuthService auth) =>
            {
                var token = auth.Register(dto.Username, dto.Password);
                return Results.Ok(new { token });
            })
            .ProducesValidationProblem()
            .WithName("RegisterUser");

            group.MapPost("/login", async ([FromBody] LoginDto dto, IAuthService auth) =>
            {
                var token = auth.Login(dto.Username, dto.Password);
                return Results.Ok(new { token });
            })
            .ProducesValidationProblem()
            .WithName("LoginUser");
        }

    }
}
