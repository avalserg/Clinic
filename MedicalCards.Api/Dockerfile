#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedicalCards.Api/MedicalCards.Api.csproj", "MedicalCards.Api/"]
COPY ["MedicalCards.Application/MedicalCards.Application.csproj", "MedicalCards.Application/"]
COPY ["MedicalCards.Domain/MedicalCards.Domain.csproj", "MedicalCards.Domain/"]
COPY ["MedicalCards.ExternalProviders/MedicalCards.ExternalProviders.csproj", "MedicalCards.ExternalProviders/"]
COPY ["MedicalCards.Persistence/MedicalCards.Persistence.csproj", "MedicalCards.Persistence/"]
RUN dotnet restore "./MedicalCards.Api/./MedicalCards.Api.csproj"
COPY . .
WORKDIR "/src/MedicalCards.Api"
RUN dotnet build "./MedicalCards.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MedicalCards.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalCards.Api.dll"]