namespace NadinSoft.Application.Features.Products
{
    public record ProductDTO(Guid Id, string Name, string ManufactureEmail, string? ManufacturePhone = "");
}
