using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;

namespace NadinSoft.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string ManufacturePhone) : ICommand<Result<ProductDTO>>
    {
    }
}
