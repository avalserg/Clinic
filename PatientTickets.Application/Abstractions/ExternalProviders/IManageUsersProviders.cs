using PatientTickets.Application.DTOs.ExternalProviders;

namespace PatientTickets.Application.Abstractions.ExternalProviders
{
    public interface IManageUsersProviders
    {
        Task<GetDoctorDto> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken);


    }
}
