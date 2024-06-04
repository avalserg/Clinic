using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reviews.Application.Abstractions.Mappings;
using Reviews.Domain.Entities;

namespace Reviews.Application.DTOs
{
    public class GetReviewDto:IMapFrom<Review>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
