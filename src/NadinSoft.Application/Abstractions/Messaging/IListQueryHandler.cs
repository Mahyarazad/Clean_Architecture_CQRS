using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    public interface IListQueryHandler<TQuery, TItem> : IRequestHandler<TQuery, TItem>
           where TQuery : IListQuery<TItem>
    {
    }
}

