using FluentResults;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string ManufactureEmail, string ManufacturePhone) : ICommand<Product>
    {
    }
}
