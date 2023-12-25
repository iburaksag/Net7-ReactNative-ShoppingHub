using System;
using FluentValidation;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application.Validations
{
    public class BasketDetailValidator : AbstractValidator<BasketDetail>
    {
        public BasketDetailValidator()
        {
            RuleFor(detail => detail.BasketId)
                .NotEmpty().WithMessage("BasketId is required.");

            RuleFor(detail => detail.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(detail => detail.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}

