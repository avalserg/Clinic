using FluentValidation;
using ManageUsers.Application.Behavior;
using ManageUsers.Application.Caches.Administrator;
using ManageUsers.Application.Caches.ApplicationUserMemoryCache;
using ManageUsers.Application.Caches.Doctors;
using ManageUsers.Application.Caches.Patients;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ManageUsers.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
    }
    public static IServiceCollection AddAuthApplication(this IServiceCollection services, IConfiguration configuration)
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
            .AddSingleton<AdministratorMemoryCache>()
            .AddSingleton<AdministratorsListMemoryCache>()
            .AddSingleton<AdministratorsCountMemoryCache>()
            .AddSingleton<ApplicationUserMemoryCache>()
        .AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("localhost", "/", h =>
                {

                    h.Username("guest");
                    h.Password("guest");
                });
                configurator.ConfigureEndpoints(context);
            });
        });

    }

}