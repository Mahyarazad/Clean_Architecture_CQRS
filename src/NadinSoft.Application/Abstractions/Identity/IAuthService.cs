using FluentResults;
using NadinSoft.Application.Features.Identity.Commands.Login;
using NadinSoft.Application.Features.Identity.Commands.Register;

namespace NadinSoft.Application.Abstractions.Identity
{
    public interface IAuthService
    {
        Task<Result<LoginCommandResult>> Login(LoginCommand command, CancellationToken cancellationToken = default);
        Task<Result<RegisterCommandResult>> Register(RegisterCommand command, CancellationToken cancellationToken = default);
    }
}
