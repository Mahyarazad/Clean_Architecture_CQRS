using FluentResults;
using FluentValidation;
using NadinSoft.Application.Abstractions.Identity;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Application.Helpers;

namespace NadinSoft.Application.Features.Identity.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<LoginCommandResult>>
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginCommand> _validator;

        public LoginCommandHandler(IAuthService authService, IValidator<LoginCommand> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        public async Task<Result<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if(validationResult.IsValid)
            {
                return await _authService.Login(request, cancellationToken);    
            }

            return Result.Fail(ResultErrorParser.GetErrorsFromValidator(validationResult));
        }
    }
}
