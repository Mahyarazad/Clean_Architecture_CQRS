using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<Result>
    {
    }
}
