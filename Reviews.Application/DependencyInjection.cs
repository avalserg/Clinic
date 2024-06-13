using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reviews.Application.Behavior;
using System.Reflection;

namespace Reviews.Application;

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
            ;

    }
}