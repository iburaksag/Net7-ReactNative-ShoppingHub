using FluentValidation;

namespace ShoppingHub.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(product => product.ProductCode)
                .MaximumLength(50).WithMessage("Product code cannot exceed 50 characters.");

            RuleFor(product => product.UnitPrice)
                .NotEmpty().WithMessage("Unit price is required.")
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

            RuleFor(product => product.Category)
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

            RuleFor(product => product.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        }
    }
}

