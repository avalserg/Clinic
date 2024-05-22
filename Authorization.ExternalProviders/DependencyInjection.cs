using Authorization.Application.Abstractions.ExternalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<IApplicationUsersProviders, ApplicationUsersProvider>();
    }
}