using FluentValidation;
using MediatR;
using MedicalCards.Application.Behavior;
using MedicalCards.Application.Caches;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MedicalCards.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
    }
    public static IServiceCollection AddAuthApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<MedicalCardMemoryCache>()
            .AddSingleton<MedicalCardsCountMemoryCache>()
            .AddSingleton<MedicalCardsListMemoryCache>()
            //.AddSingleton<DoctorsListMemoryCache>()
            //.AddSingleton<DoctorsCountMemoryCache>()
            //.AddSingleton<DoctorMemoryCache>()
            //.AddSingleton<AdministratorMemoryCache>()
            //.AddSingleton<AdministratorsListMemoryCache>()
            //.AddSingleton<AdministratorsCountMemoryCache>()
            //.AddSingleton<ApplicationUserMemoryCache>()


            ;

    }
}