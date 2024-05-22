using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientTickets.Application.Abstractions.Persistence;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;
using PatientTickets.Persistence.Repositories;

namespace PatientTickets.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
            //.AddScoped(typeof(IAsyncRead<>), typeof(AsyncRead<>))
            //.AddScoped<IContextTransaction, ContextTransaction>()
            .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
            .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
            .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
    }
}