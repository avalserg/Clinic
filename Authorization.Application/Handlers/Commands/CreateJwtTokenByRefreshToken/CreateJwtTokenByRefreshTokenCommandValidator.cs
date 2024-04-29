using Authorization.Application.ValidatorsExtensions;
using FluentValidation;

namespace Authorization.Application.Handlers.Commands.CreateJwtTokenByRefreshToken;

internal class CreateJwtTokenByRefreshTokenCommandValidator : AbstractValidator<CreateJwtTokenByRefreshTokenCommand>
{
    public CreateJwtTokenByRefreshTokenCommandValidator()
    {
        RuleFor(e => e.RefreshToken).NotEmpty().IsGuid();
    }
}