using AutoMapper;
using Microsoft.Extensions.Logging;
using Reviews.Application.Abstractions.Messaging;
using Reviews.Application.Abstractions.Persistence.Repository.Writing;
using Reviews.Application.Caches;
using Reviews.Application.DTOs;
using Reviews.Domain.Entities;
using Reviews.Domain.Errors;
using Reviews.Domain.Shared;

namespace Reviews.Application.Handlers.Commands.CreateReview;

public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, CreateReviewDto>
{
    private readonly IBaseWriteRepository<Review> _reviews;
    private readonly IMapper _mapper;
    private readonly ReviewsListMemoryCache _listCache;
    private readonly ILogger<CreateReviewCommandHandler> _logger;
    private readonly ReviewsCountMemoryCache _countCache;
    private readonly ReviewMemoryCache _reviewMemoryCache;

    public CreateReviewCommandHandler(
        IBaseWriteRepository<Review> reviews,
        
        IMapper mapper,
        ReviewsListMemoryCache listCache,
        ILogger<CreateReviewCommandHandler> logger,
        ReviewsCountMemoryCache countCache,
        ReviewMemoryCache reviewMemoryCache)
    {
       
        _reviews = reviews;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _reviewMemoryCache = reviewMemoryCache;
        
    }

    public async Task<Result<CreateReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
      
        var newReviewGuid = Guid.NewGuid();

        // var userRole = await _userRole.AsAsyncRead().FirstOrDefaultAsync(r=>r.Name=="Patient",cancellationToken);
     

        var review = Review.Create(
            newReviewGuid,
            request.PatientId,
            request.Description);
        
        review = await _reviews.AddAsync(review.Value, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _reviewMemoryCache.Clear();
        _logger.LogInformation($"New Review {review.Value.Id} created.");

        return _mapper.Map<CreateReviewDto>(review.Value);
    }
}