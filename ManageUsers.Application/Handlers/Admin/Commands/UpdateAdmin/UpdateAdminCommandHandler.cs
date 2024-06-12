using AutoMapper;
using ManageUsers.Application.Abstractions.Messaging;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Persistence.Repository.Writing;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.Caches.Administrator;
using ManageUsers.Application.DTOs.Admin;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.Errors;
using ManageUsers.Domain.Exceptions.Base;
using ManageUsers.Domain.Shared;
using ManageUsers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ManageUsers.Application.Handlers.Admin.Commands.UpdateAdmin;

internal class UpdateAdminCommandHandler : ICommandHandler<UpdateAdminCommand, GetAdminDto>
{
    private readonly IBaseWriteRepository<Administrator> _administrators;
    private readonly IMapper _mapper;
    private readonly AdministratorsListMemoryCache _listCache;
    private readonly ILogger<UpdateAdminCommandHandler> _logger;
    private readonly AdministratorsCountMemoryCache _countCache;
    private readonly AdministratorMemoryCache _administratorMemoryCache;
    private readonly ICurrentUserService _currentUserService;

    public UpdateAdminCommandHandler(
        IBaseWriteRepository<Administrator> administrators,
        IBaseWriteRepository<Domain.ApplicationUser> users,
        IMapper mapper,
        AdministratorsListMemoryCache listCache,
        ILogger<UpdateAdminCommandHandler> logger,
        AdministratorsCountMemoryCache countCache,
        AdministratorMemoryCache administratorMemoryCache,
        IBaseReadRepository<ApplicationUserRole> userRole,
        ICurrentUserService currentUserService)
    {
        _administrators = administrators;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _administratorMemoryCache = administratorMemoryCache;
        _currentUserService = currentUserService;
    }

    public async Task<Result<GetAdminDto>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {

        var administrator = await _administrators.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (administrator is null)
        {
            return Result.Failure<GetAdminDto>(DomainErrors.AdministratorDomainErrors.NotFound(request.Id));
        }

        if (request.Id != _currentUserService.CurrentUserId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic);

        if (fullName.IsFailure)
        {

            return Result.Failure<GetAdminDto>(fullName.Error);
        }

        administrator.Update(fullName.Value);
        administrator = await _administrators.UpdateAsync(administrator, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _administratorMemoryCache.Clear();
        _logger.LogInformation($"New user {administrator.Id} updated.");

        return _mapper.Map<GetAdminDto>(administrator);
    }
}