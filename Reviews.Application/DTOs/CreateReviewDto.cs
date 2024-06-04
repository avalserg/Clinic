using Reviews.Application.Abstractions.Mappings;
using Reviews.Domain.Entities;

namespace Reviews.Application.DTOs
{
    public class CreateReviewDto : IMapFrom<Review>
    {
    
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
