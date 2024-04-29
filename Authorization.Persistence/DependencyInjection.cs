using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Abstractions.Persistence.Repository.Read;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Persistence;

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