using AutoFixture;
using AutoMapper;
using Moq;
using Reviews.Application.Abstractions.Caches;
using Reviews.Application.Abstractions.Persistence.Repository.Read;
using Reviews.Application.DTOs;
using Reviews.Application.Handlers.Queries.GetReview;
using Reviews.Domain.Entities;
using Reviews.UnitTests.Core.Fixtures;
using System.Linq.Expressions;
using Reviews.UnitTests.Core;
using Xunit;
using Xunit.Abstractions;
using MediatR;

namespace Reviews.UnitTests.Queries.GetReview
{
    public class GetReviewHandlerTests : RequestHandlerTestBase<GetReviewQuery, GetReviewDto>
    {
        private readonly Mock<IBaseReadRepository<Review>> _usersMok = new();

        private readonly Mock<IReviewCache> _mockUserMemoryCache = new();

        private readonly IMapper _mapper;

        public GetReviewHandlerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetReviewQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetReviewQuery, GetReviewDto> CommandHandler =>
            new GetReviewQueryHandler(_usersMok.Object, _mapper, _mockUserMemoryCache.Object);

        [Fact]
        public async Task Should_BeValid_When_UserFounded()
        {
            // arrange
            var reviewId = Guid.NewGuid();
            var query = new GetReviewQuery()
            {
                Id = reviewId
            };

            var review = TestFixture.Build<Review>().Create();
            //  review.Id = reviewId;

            _usersMok
                .Setup(p =>
                    p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>(), default)
                ).ReturnsAsync(review);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
