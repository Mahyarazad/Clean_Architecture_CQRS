using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string ManufacturePhone) : ICommand<Result<ProductDTO>>
    {
    }
}
