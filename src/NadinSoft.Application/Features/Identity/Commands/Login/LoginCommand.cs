using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Features.Identity.Commands.Login
{
    public record LoginCommand(string EmailOrUser, string Password) : ICommand<Result<LoginCommandResult>> { }

}
