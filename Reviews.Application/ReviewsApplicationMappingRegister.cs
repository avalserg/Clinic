using Reviews.Application.BaseRealizations;

namespace Reviews.Application;

public class ReviewsApplicationMappingRegister : MappingRegister
{
    public ReviewsApplicationMappingRegister() : base(typeof(ReviewsApplicationMappingRegister).Assembly)
    {
    }
}
