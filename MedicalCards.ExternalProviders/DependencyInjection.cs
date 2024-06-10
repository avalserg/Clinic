using MedicalCards.Application.Abstractions.ExternalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalCards.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<IManageUsersProviders, ManageUsersGrpcProvider>();
    }
}