using ManageUsers.Application.Abstractions.Persistence;

namespace ManageUsers.Api;

public static class WebApplicationExtensions
{
    public static WebApplication RunDbMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();

        var migrationAttemptsCount = 0;
        while (dataContext.GetPendingMigrations().Any())
        {
            migrationAttemptsCount++;
            try
            {
                if (dataContext.GetPendingMigrations().Any())
                {
                    dataContext.Migrate();
                }
            }
            catch (Exception ex)
            {

                if (migrationAttemptsCount == 10)
                {
                    throw;
                }

                app.Logger.LogWarning("{ex.Message}", ex);
                Thread.Sleep(2000);
            }
        }
        return app;
    }

}