using Reviews.Application.DTOs.ExternalProviders;

namespace Reviews.Application.Abstractions.ExternalProviders
{
    public interface IManageUsersProviders
    {
        Task<GetPatientDto> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken);


    }
}
