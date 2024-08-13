using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NadinSoft.Application.Abstractions.Messaging;
using NadinSoft.Domain.Abstractions.Persistence.Data;
using NadinSoft.Domain.Abstractions.Persistence.Repositories;
using System.Net;

namespace NadinSoft.Application.Features.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<DeleteProductCommand> _validator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IValidator<DeleteProductCommand> validator, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _validator = validator;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(validationResult.IsValid)
            {
                if(!await _productRepository.AnyAsync(request.Id))
                {   
                    return Result.Fail(HttpStatusCode.NotFound.ToString());
                }

                var username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Uid");
                var email = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

                if(username is null)
                {
                    return Result.Fail(HttpStatusCode.Unauthorized.ToString());
                }

                _ = Guid.TryParse(username.Value.ToString(), out Guid userId);
                if(!await _productRepository.DoesUserOwnThisProductAsync(request.Id, userId, cancellationToken))
                {
                    return Result.Fail(HttpStatusCode.Forbidden.ToString());
                }

                
                var deleteResult = await _productRepository.DeleteAsync(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Fail(HttpStatusCode.Forbidden.ToString());

            }

            return Result.Fail(HttpStatusCode.BadRequest.ToString());
        }
    }
}
