using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> where TRequest : IQuery<TResponse>
    {
    }
}
