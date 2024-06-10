using Reviews.Application.Abstractions.Mappings;
using Reviews.Domain.Entities;

namespace Reviews.Application.DTOs
{
    public class GetReviewDto : IMapFrom<Review>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
