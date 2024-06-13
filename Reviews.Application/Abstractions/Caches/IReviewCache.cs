using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Application.Abstractions.Caches
{
    public interface IReviewCache : IBaseCache<Result<GetReviewDto>>
    {
    }
}
