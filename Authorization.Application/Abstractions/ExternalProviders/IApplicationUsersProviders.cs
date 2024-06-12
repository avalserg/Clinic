using Authorization.Application.Models;

namespace Authorization.Application.Abstractions.ExternalProviders
{
    public interface IApplicationUsersProviders
    {
        Task<GetApplicationUserDto?> GetApplicationUserAsync(string login, string password, CancellationToken cancellationToken);
        Task<GetApplicationUserDto> GetApplicationUserByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
