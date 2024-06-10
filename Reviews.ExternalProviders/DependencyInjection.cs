using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.Abstractions.ExternalProviders;

namespace Reviews.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<IManageUsersProviders, ManageUsersGrpcProvider>();
    }
}