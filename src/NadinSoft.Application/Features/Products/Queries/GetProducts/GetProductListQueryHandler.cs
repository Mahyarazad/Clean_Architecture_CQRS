using AutoMapper;
using MediatR;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using System.Linq;

namespace NadinSoft.Application.Features.Products.Queries.GetProducts
{
    public class GetProductListQueryHandler : IListQueryHandler<GetProductListQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var repoResult = await _productRepository.GetProductListAsync(request.pageNumber, request.pageSize,
                request.NameFilter, request.ManufactureEmailFilter, request.PhoneFilter);

            if(repoResult.Any())
            {
                return _mapper.Map<List<ProductDTO>>(repoResult);
            }

            return Enumerable.Empty<ProductDTO>();
        }
    }
}
