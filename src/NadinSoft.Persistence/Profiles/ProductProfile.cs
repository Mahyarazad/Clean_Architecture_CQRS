using AutoMapper;
using NadinSoft.Domain.Entities.Product;
using NadinSoft.Application.Features.Products;
namespace NadinSoft.Persistence.Profiles
{
    internal class ProductProfile : Profile, IProfile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }

    internal interface IProfile
    {
    }
}
