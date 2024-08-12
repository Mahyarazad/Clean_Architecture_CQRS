using FluentResults;
using FluentValidation;
using NadinSoft.Application.Abstractions.Identity;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Application.Helpers;

namespace NadinSoft.Application.Features.Identity.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<RegisterCommandResult>>
    {
        private readonly IAuthService _authService;
        private readonly IValidator<RegisterCommand> _validator;
        public RegisterCommandHandler(IAuthService authService, IValidator<RegisterCommand> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        public async Task<Result<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if(validationResult.IsValid)
            {
                return await _authService.Register(request, cancellationToken); 
            }

            return Result.Fail(ResultErrorParser.GetErrorsFromValidator(validationResult));
        }
    }
}
