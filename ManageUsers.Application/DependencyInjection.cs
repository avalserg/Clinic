using FluentValidation;
using ManageUsers.Application.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ManageUsers.Application.Caches;

namespace ManageUsers.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
    }
    public static IServiceCollection AddAuthApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<PatientMemoryCache>()
            .AddSingleton<PatientsCountMemoryCache>()
            .AddSingleton<PatientsListMemoryCache>()
            .AddSingleton<DoctorsListMemoryCache>()
            .AddSingleton<DoctorsCountMemoryCache>()
            .AddSingleton<DoctorMemoryCache>()
            
            ; 

    }
}