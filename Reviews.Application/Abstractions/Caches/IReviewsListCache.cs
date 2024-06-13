using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Application.Abstractions.Caches
{
    public interface IReviewsListCache : IBaseCache<Result<BaseListDto<GetReviewDto>>>
    {
    }
}
