using Reviews.Application.BaseRealizations;
using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Application.Caches;

public class ReviewsListMemoryCache : BaseCache<Result<BaseListDto<GetReviewDto>>>;