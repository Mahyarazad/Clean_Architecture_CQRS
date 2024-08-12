using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
