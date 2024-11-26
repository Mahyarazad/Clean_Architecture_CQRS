using NadinSoft.Application.Abstractions.Messaging;
namespace NadinSoft.Application.Features.Products.Queries.GetProducts
{
    public record GetProductListQuery(int pageNumber, int pageSize,
        string? NameFilter, string? ManufactureEmailFilter, string? PhoneFilter) : IListQuery<IEnumerable<ProductDTO>>
    {
    }
}
