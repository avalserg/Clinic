using Authorization.Application.Abstractions.Messaging;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.Abstractions.Service;
using Authorization.Domain;
using Authorization.Domain.Errors;
using Authorization.Domain.Shared;

namespace Authorization.Application.Handlers.Commands.DeleteRefreshToken
{
    internal class DeleteRefreshTokenCommandHandler : ICommandHandler<DeleteRefreshTokenCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
        public DeleteRefreshTokenCommandHandler(
            ICurrentUserService currentUserService,
            IBaseWriteRepository<RefreshToken> refreshTokens)
        {
            _currentUserService = currentUserService;
            _refreshTokens = refreshTokens;
        }
        public async Task<Result> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {

            if (_currentUserService.CurrentUserId == null)
            {
                return Result.Failure(
                    DomainErrors.RefreshTokenDomainErrors.CurrentUserIdNotFound);
            }

            var query = _refreshTokens.AsQueryable().Where(e => e.ApplicationUserId == _currentUserService.CurrentUserId);
            var tokensResult = await _refreshTokens.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            if (tokensResult.Length == 0)
            {
                return Result.Failure(
                    DomainErrors.RefreshTokenDomainErrors.RefreshTokenWithOwnerNotFound(_currentUserService.CurrentUserId));
            }
            await _refreshTokens.RemoveRangeAsync(tokensResult, cancellationToken);

            return Result.Success();
        }
    }
}
