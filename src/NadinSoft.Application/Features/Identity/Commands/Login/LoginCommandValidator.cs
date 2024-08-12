using FluentValidation;

namespace NadinSoft.Application.Features.Identity.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.EmailOrUser).NotEmpty().WithMessage("User or Email is required");
        }
    }
}
