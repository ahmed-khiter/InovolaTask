# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inovola.Weather.API/Inovola.Weather.API.csproj", "Inovola.Weather.API/"]
COPY ["Inovola.Weather.Application/Inovola.Weather.Application.csproj", "Inovola.Weather.Application/"]
COPY ["Inovola.Weather.Domain/Inovola.Weather.Domain.csproj", "Inovola.Weather.Domain/"]
COPY ["Inovola.Weather.Infrastructure/Inovola.Weather.Infrastructure.csproj", "Inovola.Weather.Infrastructure/"]
RUN dotnet restore "./Inovola.Weather.API/Inovola.Weather.API.csproj"
COPY . .
WORKDIR "/src/Inovola.Weather.API"
RUN dotnet build "./Inovola.Weather.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Inovola.Weather.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inovola.Weather.API.dll"]