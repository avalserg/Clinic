using MedicalCards.Application.DTOs.ExternalProviders;

namespace MedicalCards.Application.Abstractions.ExternalProviders
{
    public interface IManageUsersProviders
    {
        Task<GetPatientDto> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken);


    }
}
