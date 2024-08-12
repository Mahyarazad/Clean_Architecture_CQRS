using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
    }
}
