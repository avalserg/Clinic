using Authorization.Api;
using Authorization.Api.Middlewares;
using Authorization.Api.OptionsSetup;
using Authorization.Application;
using Authorization.Application.Middlewares;
using Authorization.ExternalProviders;
using Authorization.Persistence;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text.Json.Serialization;

try
{
    const string appPrefix = "authorization";
    const string version = "v1";
    const string appName = "authorization API v1";

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
    builder.Services.ConfigureOptions<JwtOptionsSetup>();

    builder.Services
        .AddSwaggerWidthJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName)
        //.AddSwaggerForControllersWidthJwtAuth()
        .AddCoreApiServices()
        .AddCoreApplicationServices()
        .AddCoreAuthApiServices(builder.Configuration)
        .AddPersistenceServices(builder.Configuration)
        .AddCoreAuthServices()
        .AddExternalProviders()
        .AddHttpClient()
        .AddAllCors()
        .AddAuthApplication()
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddEndpointsApiExplorer();
    var app = builder.Build();

    app.RunDbMigrations();
    //.RegisterApis(Assembly.GetExecutingAssembly(), $"{appPrefix}/api/{version}");


    app.UseCoreExceptionHandler()
        .UseAuthExceptionHandler()
        .UseAuthentication()
        .UseAuthorization()
        .UseHttpsRedirection();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c => c.RouteTemplate = appPrefix + "/swagger/{documentname}/swagger.json");
        app.UseSwaggerUI(options =>
        {

            options.SwaggerEndpoint("/" + appPrefix + $"/swagger/{version}/swagger.json", version);
            options.RoutePrefix = appPrefix + "/swagger";
        });
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }
    //app.UseCors(CorsPolicy.AllowAll);
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        //.WithOrigins("https://localhost:3000)
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
