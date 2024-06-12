using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Attributes;
using MediatR;
using System.Reflection;

namespace ManageUsers.Application.Behavior;

public class AuthorizePermissionsBehavior<TRequest, TResponse>(ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.GetType().GetCustomAttribute(typeof(RequestAuthorizeAttribute), true) is not RequestAuthorizeAttribute requestAuthorizeAttribute)
        {
            return next();
        }


        if (requestAuthorizeAttribute.Roles is null || requestAuthorizeAttribute.Roles.Length == 0)
        {
            return next();
        }

        var requiredRoles = requestAuthorizeAttribute.Roles;
        if (requiredRoles == null || requiredRoles.Length == 0)
        {
            return next();
        }

        return next();
    }
}