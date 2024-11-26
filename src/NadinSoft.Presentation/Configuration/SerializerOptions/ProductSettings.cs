using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using System.Text.Json.Serialization;

namespace NadinSoft.Presentation.Configuration.SerializerOptions
{
    [JsonSerializable(typeof(CreateProductCommand))]
    [JsonSerializable(typeof(CreateProductResponse))]
    public partial class ProductSettings : JsonSerializerContext
    {
    }
}
