using FluentValidation;
using Store.Product.Api.CQRS.Commands;

namespace Store.Product.Api
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Title).MinimumLength(3);
            RuleFor(c => c.Title).MaximumLength(500);
        }
    }
}
