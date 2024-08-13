using AutoMapper;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Application.Helpers;
using NadinSoft.Domain.Abstractions.Persistence.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using NadinSoft.Domain.Entities.Product;

namespace NadinSoft.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IValidator<CreateProductCommand> validator, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _validator = validator;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Uid");
            var email = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            if(username is null)
            {
                Result.Fail("You need to login");
            }

            _ = Guid.TryParse(username!.Value.ToString(), out Guid userId);

            var requestValidation = await _validator.ValidateAsync(request);
            if(requestValidation.IsValid)
            {
                var result = Product.Create(request.Name, email!.Value, request.ManufacturePhone, userId);
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
