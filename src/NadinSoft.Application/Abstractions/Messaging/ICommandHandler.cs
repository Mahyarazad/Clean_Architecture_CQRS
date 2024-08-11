using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand: ICommand<TResponse>
    {
    }
}
