using MedicalCards.Application.Abstractions.Mappings;

namespace MedicalCards.Application.DTOs.MedicalCard
{
    public class GetMedicalCardDto : IMapFrom<Domain.MedicalCard>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Patronymic { get; set; } = default!;
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
