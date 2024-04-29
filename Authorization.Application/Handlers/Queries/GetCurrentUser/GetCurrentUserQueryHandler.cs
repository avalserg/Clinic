using Authorization.Application.Abstractions.Persistence.Repository.Read;
using Authorization.Application.Abstractions.Service;
using Authorization.Application.DTOs;
using Authorization.Application.Exceptions;
using Authorization.Domain;
using AutoMapper;
using MediatR;

namespace Authorization.Application.Handlers.Queries.GetCurrentUser;

internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetUserDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IBaseReadRepository<ApplicationUser> users, 
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _users = users;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<GetUserDto> Handle(GetCurrentUserQuery request,  CancellationToken cancellationToken)
    {
        var user = await _users.AsAsyncRead()
            .SingleOrDefaultAsync(e => e.ApplicationUserId == _currentUserService.CurrentUserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with id {_currentUserService.CurrentUserId}");
        }

        return _mapper.Map<GetUserDto>(user);
    }
}