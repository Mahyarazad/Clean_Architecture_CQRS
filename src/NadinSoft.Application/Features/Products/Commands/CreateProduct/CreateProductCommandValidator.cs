using FluentValidation;

namespace NadinSoft.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p=> p.ManufactureEmail).NotEmpty().EmailAddress();
        }
    }
}
