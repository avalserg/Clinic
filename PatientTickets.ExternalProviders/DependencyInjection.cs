using Microsoft.Extensions.DependencyInjection;
using PatientTickets.Application.Abstractions.ExternalProviders;

namespace PatientTickets.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<IManageUsersProviders, ManageUsersGrpcProvider>();
    }
}