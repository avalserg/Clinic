#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientTickets.Api/PatientTickets.Api.csproj", "PatientTickets.Api/"]
COPY ["PatientTickets.Application/PatientTickets.Application.csproj", "PatientTickets.Application/"]
COPY ["PatientTickets.Domain/PatientTickets.Domain.csproj", "PatientTickets.Domain/"]
COPY ["PatientTickets.ExternalProviders/PatientTickets.ExternalProviders.csproj", "PatientTickets.ExternalProviders/"]
COPY ["PatientTickets.Persistence/PatientTickets.Persistence.csproj", "PatientTickets.Persistence/"]
RUN dotnet restore "./PatientTickets.Api/./PatientTickets.Api.csproj"
COPY . .
WORKDIR "/src/PatientTickets.Api"
RUN dotnet build "./PatientTickets.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientTickets.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientTickets.Api.dll"]