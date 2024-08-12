using AutoMapper;
using FluentResults;
using FluentValidation;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Application.Helpers;
using NadinSoft.Domain.Abstractions.Persistence.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, Result<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;

        public CreateProductHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<CreateProductCommand> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var requestValidation = await _validator.ValidateAsync(request);
            if(requestValidation.IsValid)
            {
                var result = Product.Create(request.Name, request.ManufactureEmail, request.ManufacturePhone);
                if(result.IsSuccess)
                {
                    var resultDTO = _mapper.Map<ProductDTO>(result.Value);
                    await _productRepository.AddAsync(result.Value, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Result.Ok(resultDTO);
                }

                return Result.Fail(result.Errors);
            }


            return Result.Fail(ResultErrorParser.GetErrorsFromValidator(requestValidation));
        }
    }
}
