using System;
using FluentValidation;
using ShoppingHub.Application.Products.Commands.CreateProduct;

namespace ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail
{
    public class CreateBasketDetailCommandValidator : AbstractValidator<CreateBasketDetailCommand>
    {
        public CreateBasketDetailCommandValidator()
        {
            RuleFor(bd => bd.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        }
    }
}

