using Asp.Versioning;
using Reviews.Api;
using Reviews.Api.Middlewares;
using Reviews.Application;
using Reviews.Application.Middlewares;
using Reviews.ExternalProviders;
using Reviews.Infrastructure;
using Reviews.Persistence;
using Serilog;
using Serilog.Events;
using System.Reflection;

try
{
    const string appPrefix = "Reviews";
    const string version = "v1";
    const string appName = "Reviews API v1";

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
#if DEBUG
        .WriteTo.Console()
#endif
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Information-.txt", LogEventLevel.Information,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Warning-.txt", LogEventLevel.Warning,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Error-.txt", LogEventLevel.Error,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30, buffered: true));


    builder.Services
        .AddSwaggerWidthJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName)
        .AddCoreApiServices()
        .AddCoreApplicationServices()
        .AddCoreAuthApiServices(builder.Configuration)
        .AddPersistenceServices(builder.Configuration)
        .AddCoreAuthServices()
        .AddExternalProviders()
        .AddAuthApplication()
        .AddDistributedCachesServices(builder.Configuration)
        .AddControllers()
        .AddNewtonsoftJson();
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("X-Api-Version"));
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });


    builder.Services.AddEndpointsApiExplorer();
    var app = builder.Build();

    app.RunDbMigrations();


    app.UseCoreExceptionHandler()
        .UseAuthExceptionHandler()
        .UseAuthentication()
        .UseAuthorization();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentname}/swagger.json"; });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            options.RoutePrefix = "swagger";
        });
    }
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true));
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    var appSettingsFile = $"{Directory.GetCurrentDirectory()}/appsettings.json";
    var settingsJson = File.ReadAllText(appSettingsFile);
    var appSettings = System.Text.Json.JsonDocument.Parse(settingsJson);
    var logsPath = appSettings.RootElement.GetProperty("Logging").GetProperty("LogsFolder").GetString();
    var logger = new LoggerConfiguration()
        .WriteTo.File($"{logsPath}/Log-Run-Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Hour,
            retainedFileCountLimit: 30)
        .CreateLogger();
    logger.Fatal(ex.Message, ex);
}
