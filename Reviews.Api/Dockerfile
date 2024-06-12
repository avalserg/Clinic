#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Reviews.Api/Reviews.Api.csproj", "Reviews.Api/"]
COPY ["Reviews.Application/Reviews.Application.csproj", "Reviews.Application/"]
COPY ["Reviews.Domain/Reviews.Domain.csproj", "Reviews.Domain/"]
COPY ["Reviews.ExternalProviders/Reviews.ExternalProviders.csproj", "Reviews.ExternalProviders/"]
COPY ["Reviews.Persistence/Reviews.Persistence.csproj", "Reviews.Persistence/"]
RUN dotnet restore "./Reviews.Api/./Reviews.Api.csproj"
COPY . .
WORKDIR "/src/Reviews.Api"
RUN dotnet build "./Reviews.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Reviews.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reviews.Api.dll"]