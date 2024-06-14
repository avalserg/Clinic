using ManageUsers.Api;
using ManageUsers.Api.GrpcServer.Doctors;
using ManageUsers.Api.GrpcServer.Patients;
using ManageUsers.Api.Middlewares;
using ManageUsers.Application;
using ManageUsers.Application.Middlewares;
using ManageUsers.Persistence;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text.Json.Serialization;

try
{
    const string appPrefix = "Users";
    const string version = "v1";
    const string appName = "Users API v1";

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
        .AddAuthApplication(builder.Configuration)
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


    builder.Services.AddGrpc().AddJsonTranscoding();
    builder.Services.AddGrpcSwagger().AddSwaggerGen();

    builder.Services.AddEndpointsApiExplorer();
    var app = builder.Build();

    app.RunDbMigrations();


    app.UseCoreExceptionHandler()
        .UseAuthExceptionHandler()
        .UseAuthentication()
        .UseAuthorization()
        .UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Avatars")),
        RequestPath = new PathString("/avatars")
    });
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
        RequestPath = new PathString("/images")
    });

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
        //.WithOrigins("https://localhost:3000))
        .SetIsOriginAllowed(origin => true));
    app.MapControllers();
    app.MapGrpcService<GrpcDoctorService>();
    app.MapGrpcService<GrpcPatientsService>();
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
