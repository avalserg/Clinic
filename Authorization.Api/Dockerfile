#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Authorization.Api/Authorization.Api.csproj", "Authorization.Api/"]
COPY ["Authorization.Application/Authorization.Application.csproj", "Authorization.Application/"]
COPY ["Authorization.Domain/Authorization.Domain.csproj", "Authorization.Domain/"]
COPY ["Authorization.ExternalProviders/Authorization.ExternalProviders.csproj", "Authorization.ExternalProviders/"]
COPY ["Authorization.Persistence/Authorization.Persistence.csproj", "Authorization.Persistence/"]
RUN dotnet restore "./Authorization.Api/./Authorization.Api.csproj"
COPY . .
WORKDIR "/src/Authorization.Api"
RUN dotnet build "./Authorization.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authorization.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization.Api.dll"]