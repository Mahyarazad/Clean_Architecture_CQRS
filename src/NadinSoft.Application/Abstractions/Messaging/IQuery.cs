using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
