
using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Features.Identity.Commands.Register
{
    public record RegisterCommand(string FirstName, string LastName, string Email, string UserName, string Password) 
        : ICommand<Result<RegisterCommandResult>>;

}
