using AutoMapper;
using ManageUsers.Application.Abstractions.Persistence.Repository.Read;
using ManageUsers.Application.Abstractions.Service;
using ManageUsers.Application.DTOs.ApplicationUser;
using ManageUsers.Application.DTOs.CurrentUser;
using MediatR;

namespace ManageUsers.Application.Handlers.ApplicationUser.Queries.GetCurrentUser;

internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserDto>
{
    private readonly IBaseReadRepository<Domain.ApplicationUser> _users;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IBaseReadRepository<Domain.ApplicationUser> users, 
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _users = users;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<GetCurrentUserDto> Handle(GetCurrentUserQuery request,  CancellationToken cancellationToken)
    {
        var user = await _users.AsAsyncRead()
            .SingleOrDefaultAsync(e => e.ApplicationUserId == _currentUserService.CurrentUserId, cancellationToken);

        if (user is null)
        {
            // throw new NotFoundException($"User with id {_currentUserService.CurrentUserId}");
        }

        return _mapper.Map<GetCurrentUserDto>(user);
    }
}