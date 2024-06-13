using AutoMapper;
using Microsoft.Extensions.Logging;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.Abstractions.ExternalProviders;
using Reviews.Application.Abstractions.Messaging;
using Reviews.Application.Abstractions.Persistence.Repository.Writing;
using Reviews.Application.DTOs;
using Reviews.Domain.Entities;
using Reviews.Domain.Errors;
using Reviews.Domain.Shared;

namespace Reviews.Application.Handlers.Commands.CreateReview;

public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, CreateReviewDto>
{
    private readonly IBaseWriteRepository<Review> _reviews;
    private readonly IMapper _mapper;
    private readonly IReviewsListCache _listCache;
    private readonly ILogger<CreateReviewCommandHandler> _logger;
    private readonly IReviewsCountCache _countCache;
    private readonly IReviewCache _reviewMemoryCache;
    private readonly IManageUsersProviders _applicationUsersProviders;

    public CreateReviewCommandHandler(
        IBaseWriteRepository<Review> reviews,

        IMapper mapper,
        IReviewsListCache listCache,
        ILogger<CreateReviewCommandHandler> logger,
        IReviewsCountCache countCache,
        IReviewCache reviewMemoryCache, IManageUsersProviders applicationUsersProviders)
    {

        _reviews = reviews;
        _mapper = mapper;
        _listCache = listCache;
        _logger = logger;
        _countCache = countCache;
        _reviewMemoryCache = reviewMemoryCache;
        _applicationUsersProviders = applicationUsersProviders;
    }

    public async Task<Result<CreateReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var patient = await _applicationUsersProviders.GetPatientByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            return Result.Failure<CreateReviewDto>(DomainErrors.Review.CreatorReviewNotFound(request.PatientId));
        }
        var newReviewGuid = Guid.NewGuid();

        var review = Review.Create(
            newReviewGuid,
            request.PatientId,
            patient.FirstName,
            patient.LastName,
            patient.Patronymic,
            request.Description);

        review = await _reviews.AddAsync(review.Value, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _reviewMemoryCache.Clear();
        _logger.LogInformation($"New Review {review.Value.Id} created.");

        return _mapper.Map<CreateReviewDto>(review.Value);
    }
}