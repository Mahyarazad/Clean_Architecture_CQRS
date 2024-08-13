using AutoMapper;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Application.Helpers;
using NadinSoft.Domain.Abstractions.Persistence.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
namespace NadinSoft.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<UpdateProductCommand> _validator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository productRepository, IValidator<UpdateProductCommand> validator, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _validator = validator;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!await _productRepository.AnyAsync(request.Id))
            {
                return Result.Fail($"Product with this {request.Id} doesn't exists in database");
            }

            if(validationResult.IsValid)
            {
                var username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Uid");
                var email = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

                if(username is null)
                {
                    return Result.Fail("You need to login first!");
                }

                _ = Guid.TryParse(username.Value.ToString(), out Guid userId);
                if(!await _productRepository.DoesUserOwnThisProductAsync(request.Id, userId, cancellationToken))
                {
                    return Result.Fail($"This user doesn't own this product with the requested Id: {request.Id}");
                }

                var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
                var result = existingProduct!.Update(request.Name, email!.Value, request.ManufacturePhone);
                var resultDTO = _mapper.Map<ProductDTO>(result.Value);

                await _productRepository.UpdateAsync(result.Value, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Ok(resultDTO);

            }

            return Result.Fail(ResultErrorParser.GetErrorsFromValidator(validationResult));
        }
    }
}
