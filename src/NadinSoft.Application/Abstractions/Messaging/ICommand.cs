using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
