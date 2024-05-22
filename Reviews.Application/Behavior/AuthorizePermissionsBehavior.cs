using System.Reflection;
using MediatR;
using Reviews.Application.Abstractions.Service;
using Reviews.Application.Attributes;

namespace Reviews.Application.Behavior;

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

        //if (currentUserService.CurrentUserId is null) throw new UnauthorizedException();

        if (requestAuthorizeAttribute.Roles is null || requestAuthorizeAttribute.Roles.Length == 0)
        {
            return next();
        }

        //if (currentUserService.CurrentUserRoleEnum is null)
        //{
        //    throw new ForbiddenException();
        //}

        var requiredRoles = requestAuthorizeAttribute.Roles;
        if (requiredRoles == null || requiredRoles.Length == 0)
        {
            return next();
        }
        
        //if (requiredRoles.Any(rn => currentUserService.CurrentUserRoles.All(roleEnum => roleEnum != rn)))
        //{
        //    throw new ForbiddenException();
        //}

        return next();
    }
}