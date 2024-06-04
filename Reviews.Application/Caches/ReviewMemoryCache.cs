using Reviews.Application.BaseRealizations;
using Reviews.Application.DTOs;
using Reviews.Domain.Shared;

namespace Reviews.Application.Caches;

public class ReviewMemoryCache : BaseCache<Result<GetReviewDto>>;