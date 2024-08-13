using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    public interface IListQuery<TItem> : IRequest<TItem>
    {

    }
}