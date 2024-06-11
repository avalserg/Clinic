using FluentValidation;
using MediatR;
using MedicalCards.Application.Behavior;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.Caches.MedicalCard;
using MedicalCards.Application.Caches.Prescription;
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
            .AddSingleton<AppointmentsListMemoryCache>()
            .AddSingleton<AppointmentsCountMemoryCache>()
            .AddSingleton<AppointmentMemoryCache>()
            .AddSingleton<PrescriptionMemoryCache>()
            .AddSingleton<PrescriptionsListMemoryCache>()
            .AddSingleton<PrescriptionsCountMemoryCache>();

    }
}